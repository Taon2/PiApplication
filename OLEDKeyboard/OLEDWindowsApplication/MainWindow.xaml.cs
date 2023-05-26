using System.Windows;
using System.Windows.Media;
using System.Linq;
using OLEDWindowsApplication.Models;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Button = System.Windows.Controls.Button;
using FontFamily = System.Windows.Media.FontFamily;
using Image = System.Windows.Controls.Image;
using MessageBox = System.Windows.Forms.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace OLEDWindowsApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private int defaultLayout;
        private int selectedLayout;
        private int activeLayout;

        private Layout _defaultLayout =>
            layoutManager.GetLayout(defaultLayout); // This is the designated layout with the default values

        private Layout _selectedLayout =>
            layoutManager.GetLayout(selectedLayout); // This is the layout being viewed by the UI

        private Layout _activeLayout =>
            layoutManager.GetLayout(activeLayout); // This is the layout that is active on the OLEDs

        private LayoutManager layoutManager;
        private Position _selectedKey; // This is the key the user has currently selected
        private Boolean _keyImageChanged = false;
        private Bitmap? _lastImportedImage;
        private Configuration configuration;
        private HardwareDataBridge _dataBridge;
        WinEventDelegate dele = null;
        public MainWindow(Configuration configuration, LayoutManager layoutManager, HardwareDataBridge dataBridge)
        {
            this.configuration = configuration;
            this.layoutManager = layoutManager;
            this._dataBridge = dataBridge;
            this.FontFamily = new FontFamily("Cascadia Code");
            
            InitializeComponent();
            
            UpdateUi();

            UpdateOleds();

            // set up event for window switching
            dele = WinEventProc;
            SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, dele, 0, 0, WINEVENT_OUTOFCONTEXT);
        }

        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;
        
        [DllImport("user32")]
        private static extern UInt32 GetWindowThreadProcessId(IntPtr hWnd, out Int32 lpdwProcessId);
  
        public static Int32 GetWindowProcessID(IntPtr hwnd)
        {
            Int32 pid = 1;
            GetWindowThreadProcessId(hwnd, out pid);
            return pid;
        }

        /**
         * Listen for when a window changes focus and change the oled layout where necessary
         */
        public void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            string newWindow = Process.GetProcessById(GetWindowProcessID(hwnd)).ProcessName;
            foreach (Layout layout in layoutManager.GetAllLayouts())
            {   
                if (!layout.Application.Equals("Not Selected"))
                {
                    if (layout.Application.ToLower().Equals(newWindow.ToLower()))
                    {
                        //MessageBox.Show("Layout Applied"); this for debugging purposes only
                        activeLayout = layout.Id;
                        UpdateUi();
                        UpdateOleds();    
                        break;
                    }   
                }
            }
        } 
        
        public void refreshUIButtons()
        {
            configuration.Save();
            System.Windows.Controls.Image[] toRefresh = { zeroImg, oneImg, twoImg, threeImg, fourImg, fiveImg, sixImg, sevenImg, eightImg, nineImg, plusImg, enterImg, dotImg, minusImg, starimg, slashImg, equalsImg, backImg  };
            Position[] toUse = { Position.Zero, Position.One, Position.Two, Position.Three, Position.Four, Position.Five, Position.Six, Position.Seven, Position.Eight, Position.Nine, Position.Plus, Position.Enter, Position.Dot, Position.Minus, Position.Star, Position.Slash, Position.Equals, Position.Back };
            for (int i = 0; i < toUse.Length; i++)
            {
                toRefresh[i].Source = ImageConverter.BitmapToBitmapImage(_selectedLayout.GetSource(toUse[i]));
            }
        }

        /**
         * This function updates the UI to represent the selected layout
         */
        private void UpdateUi()
        {
            configuration.Save();
            // update the name header
            LayoutName.Content = _selectedLayout.GetName().ToUpper();
            LayoutNameBox.Text = "ENTER NAME";
            // update the UI numpad button elements
            refreshUIButtons();
            // update selected app
            SelectedAppLabel.Content = layoutManager.GetLayout(selectedLayout).Application;
            BrightnessSlider.Value = layoutManager.GetLayout(selectedLayout).Brightness;
            if (_selectedLayout.GetName() == "Default")
            {
                BrightnessSlider.IsEnabled = false;
            }
            else
            {
                BrightnessSlider.IsEnabled = true;
            }
            // clear the sidebar and add all buttons in _layouts that should be there
            Sidebar.Children.Clear();

            // re-create the add button
            Button newButton = new ButtonWithId(-1);
            newButton.Name = "addButton";
            newButton.Content = new Image
            {
                Source = DefaultLayout.GetBitmapImage(DefaultLayout.AddLayoutIcon),
                Height = 36,
                Width = 36
            };
            newButton.VerticalAlignment = VerticalAlignment.Center;
            newButton.Height = 36;
            newButton.Width = 36;
            newButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x20, 0x20, 0x20));
            newButton.Foreground = System.Windows.Media.Brushes.White;
            newButton.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x20, 0x20, 0x20));

            newButton.Click += CreateNewLayout;
            Sidebar.Children.Add(newButton);

            foreach (var layout in layoutManager.layouts)
            {
                NewSidebarButton(layout);
            }
        }

        /**
         * This function updates the OLEDs with the data from the active layout
         */
        private void UpdateOleds()
        {
            foreach (var keyValuePair in _activeLayout.GetKeys())
            {
                _dataBridge.WriteKey(keyValuePair.Value);
            }
        }

        private void OpenKeySettings(object sender, RoutedEventArgs e)
        {
            if (_selectedLayout.GetName() == "Default")
            {
                System.Windows.Forms.MessageBox.Show(
                    "The Default layout cannot be modified. Create or select a new layout before modifying.",
                    "Layout Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Numpad.Visibility == Visibility.Visible)
            {
                Numpad.Visibility = Visibility.Collapsed;
                KeyLayout.Visibility = Visibility.Visible;
                LayoutOptions.Visibility = Visibility.Collapsed;
                KeyOptions.Visibility = Visibility.Visible;
                _keyImageChanged = false;
                if (sender is ButtonWithPositionEnum buttonWithPositionEnum)
                {
                    PopulateKeySettings(buttonWithPositionEnum);
                }
                else
                {
                    MessageBox.Show(
                        "An error occured when clicking this key.",
                        "UI Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Numpad.Visibility = Visibility.Visible;
                KeyLayout.Visibility = Visibility.Collapsed;
                LayoutOptions.Visibility = Visibility.Visible;
                KeyOptions.Visibility = Visibility.Collapsed;
            }
        }

        private void CancelChanges(object sender, RoutedEventArgs e)
        {
            OpenKeySettings(sender, e);
        }

        private void SaveIndivKeySettings(object sender, RoutedEventArgs e)
        {
            OpenKeySettings(sender, e);
            if (_keyImageChanged)
            {
                _selectedLayout.SetSource(_selectedKey,
                    _lastImportedImage); //this version of the pic is just for displaying in UI
                _lastImportedImage = null;

                // update the oleds if modifying the active layout  
                if (_selectedLayout == _activeLayout)
                {
                    UpdateOleds();
                }
            }

            _keyImageChanged = false;
            refreshUIButtons();
        }

        /**
         * This function creates a new layout when the + button is pressed on the sidebar
         */
        private void CreateNewLayout(object sender, RoutedEventArgs e)
        {
            var layout = layoutManager.AddLayout();
            NewSidebarButton(layout);
            UpdateUi();
        }

        private void NewSidebarButton(Layout layout)
        {
            // add new button to sidebar
            ButtonWithId newButton = new ButtonWithId(layout.Id)
            {
                Content = new Image
                {
                    Source = DefaultLayout.GetBitmapImage(layout.SidebarIcon),
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 30,
                    Width = 30
                },
                Height = 36,
                Width = 36,
                // set button to grey when layout is selected, otherwise set to default button color
                Background = selectedLayout == layout.Id ? 
                    new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x42, 0x42, 0x42)) :
                    new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x20, 0x20, 0x20)),
                Foreground = System.Windows.Media.Brushes.White,
                BorderBrush = selectedLayout == layout.Id ? 
                    new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x42, 0x42, 0x42)) :
                    new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x20, 0x20, 0x20)),
            };
            newButton.Click += SelectLayout;
            Sidebar.Children.Add(newButton);
        }

        private void RepopulateKeyImage(object sender, RoutedPropertyChangedEventArgs<double> routedPropertyChangedEventArgs)
        {
            Big_Image.Source = ImageConverter.BitmapToBitmapImage(_selectedLayout.Keys[_selectedKey].GetBitmap(routedPropertyChangedEventArgs.NewValue));
            Real_Size_Image.Source = ImageConverter.BitmapToBitmapImage(_selectedLayout.Keys[_selectedKey].GetBitmap(routedPropertyChangedEventArgs.NewValue));
        }
        
        private void PopulateKeySettings(ButtonWithPositionEnum buttonPressed)
        {
            KeyTitle.Content = $"{buttonPressed.CurrentPosition} Key";
            _selectedKey = buttonPressed.CurrentPosition;
            
            Big_Image.Source = ImageConverter.BitmapToBitmapImage(_selectedLayout.GetSource(_selectedKey));
            Real_Size_Image.Source = ImageConverter.BitmapToBitmapImage(_selectedLayout.GetSource(_selectedKey));
        }

        private void SelectLayout(object sender, RoutedEventArgs e)
        {
            selectedLayout = ((ButtonWithId)sender).id;

            if (Numpad.Visibility == Visibility.Collapsed)
            {
                // hide the key settings
                Numpad.Visibility = Visibility.Visible;
                KeyLayout.Visibility = Visibility.Collapsed;
                LayoutOptions.Visibility = Visibility.Visible;
                KeyOptions.Visibility = Visibility.Collapsed;
            }

            UpdateUi();
        }

        /**
         * This function sets the layout that is being viewed in the UI as active on the OLEDs
         */
        private void ApplyLayout(object sender, RoutedEventArgs e)
        {
            activeLayout = selectedLayout;

            UpdateUi();
            UpdateOleds();
        }

        /**
         * This function resets the layout that is being viewed in the UI to the default values
         */
        private void ResetLayout(object sender, RoutedEventArgs e)
        {
            int id = selectedLayout;

            var layout = layoutManager.GetLayout(id);
            if (_selectedLayout.GetName() == "Default")
            {
                System.Windows.Forms.MessageBox.Show(
                    "The Default layout cannot be modified. Create or select a new layout before modifying.",
                    "Layout Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string layoutName = _selectedLayout.GetName();

            //layout.SetKeys(DefaultLayout.defaultLayout);
            layout.SetKeys(DefaultLayout.CreateDefaultLayout());
            UpdateUi();

            if (_activeLayout == layout)
            {
                UpdateOleds();
            }
        }

        /**
         * This function deletes a layout when the Delete button is pressed
         */
        private void DeleteLayout(object sender, RoutedEventArgs e)
        {
            if (_selectedLayout.GetName() == "Default")
            {
                System.Windows.Forms.MessageBox.Show(
                    "The Default layout cannot be modified. Create or select a new layout before modifying.",
                    "Layout Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var layout = layoutManager.GetLayout(selectedLayout);

            // remove the layout from the list of layouts
            layoutManager.RemoveLayout(selectedLayout);

            // if the layout we are removing is selected, set UI to default (first layout)
            if (selectedLayout == layout.Id)
            {
                selectedLayout = layoutManager.layouts.First().Id;
                UpdateUi();
            }

            // if the layout we are removing is active, set active to default (first layout)
            if (activeLayout == layout.Id)
            {
                activeLayout = layoutManager.layouts.First().Id;
                UpdateOleds();
            }

            // remove the button from the sidebar
            foreach (ButtonWithId button in Sidebar.Children)
            {
                if (button.id != layout.Id) continue;
                Sidebar.Children.Remove(button);
                break;
            }
        }

        /**
         * Changes the image for an individual key
         */
        private void ChangeKeyImage(object sender, RoutedEventArgs e)
        {
            _lastImportedImage = UploadImage();
            if (_lastImportedImage != null)
            {
                _keyImageChanged = true;

                Real_Size_Image.Source = ImageConverter.BitmapToBitmapImage(_lastImportedImage);
                Big_Image.Source = ImageConverter.BitmapToBitmapImage(_lastImportedImage);
                
                UpdateUi();
            }
        }

        /**
         * Changes the image on the sidebar for the selected layout
         */
        private void SelectSidebarIcon(object sender, RoutedEventArgs e)
        {
            if (_selectedLayout.GetName() == "Default")
            {
                System.Windows.Forms.MessageBox.Show("The Default layout cannot be modified. Create or select a new layout before modifying.", "Layout Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Bitmap _sidebarIcon = UploadImage();
            if (_sidebarIcon != null)
            {
                _selectedLayout.SidebarIcon = DefaultLayout.GetColorList(_sidebarIcon);
            }
            
            UpdateUi();
        }

        /**
         * Gathers an image from an upload image display
         */
        private Bitmap? UploadImage()
        {
            string filePath = string.Empty;

            var dialog = new OpenFileDialog
            {
                FileName = "Document", // Default file name
                DefaultExt = ".png", // Default file extension
                Filter = "Image Files(JPG, PNG)|*.JPG;*.PNG|All files (*.*)|*.*" // Filter files by extension
            };

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                filePath = dialog.FileName;
            }

            if (filePath != "")
            {
            Bitmap bitmapSource = new Bitmap(filePath);
                Bitmap bitmap = new Bitmap(bitmapSource, 64, 64);
                return bitmap;
            }

            return null;
        }

        /**
         * This function applies the name entered into the text box for a layout
         */
        private void ApplyName(object sender, RoutedEventArgs e)
        {
            if (_selectedLayout.GetName() == "Default")
            {
                System.Windows.Forms.MessageBox.Show(
                    "The Default layout cannot be modified. Create or select a new layout before modifying.",
                    "Layout Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var input = LayoutNameBox.Text;
            
            if (input == "Enter name here")
            {
                return;
            }

            if (layoutManager.GetLayout(input) != null)
            {
                System.Windows.Forms.MessageBox.Show("A layout with that name already exists.", "Layout Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // update the _layout list with the new name
            layoutManager.GetLayout(selectedLayout).Name = input;

            // update the UI to display the new name
            UpdateUi();
        }

        /**
         * This function will open the help/about page of the application
         */
        private void HelpButtonClick(object sender, RoutedEventArgs e)
        {
            HelpWindow win = new HelpWindow();
            win.Show();
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            if (LayoutNameBox.Text == "ENTER NAME")
            {
                LayoutNameBox.Text = "";
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            if (LayoutNameBox.Text == "")
            {
                LayoutNameBox.Text = "ENTER NAME";
            }
        }

        private void SelectApp(object sender, RoutedEventArgs e)
        {
            if (_selectedLayout.GetName() == "Default")
            {
                System.Windows.Forms.MessageBox.Show("The Default layout cannot be modified. Create or select a new layout before modifying.","Layout Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dialog = new OpenFileDialog
            {
                FileName = "Executable", // Default file name
                DefaultExt = ".exe", // Default file extension
                Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*" // Filter files by extension
            };
            
            string filePath = string.Empty;
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                filePath = dialog.FileName;
                layoutManager.GetLayout(selectedLayout).Application = System.IO.Path.GetFileNameWithoutExtension(filePath);
                SelectedAppLabel.Content = System.IO.Path.GetFileNameWithoutExtension(filePath);
            }
        }

        private void ClearApp(object sender, RoutedEventArgs e)
        {
            layoutManager.GetLayout(selectedLayout).Application = "Not Selected";
            UpdateUi();
        }

        private void BrightnessSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_selectedLayout.GetName() != "Default")
            {
                layoutManager.GetLayout(selectedLayout).SetBrightness(BrightnessSlider.Value); 
                RepopulateKeyImage(sender, e);
                refreshUIButtons();
            }
        }

        private void ApplyKey(object sender, RoutedEventArgs e)
        {
            Key tmp = new Key();
            tmp.SetChipSelect(_selectedLayout.GetKeys()[_selectedKey].CSString);
            if (_lastImportedImage != null)
            {
                tmp.SetBitmap(_lastImportedImage);
            }
            else
            {
                tmp.SetBitmap(_selectedLayout.GetKeys()[_selectedKey].GetBitmap());
            }
            _dataBridge.WriteKey(tmp);
        }
    }
}
