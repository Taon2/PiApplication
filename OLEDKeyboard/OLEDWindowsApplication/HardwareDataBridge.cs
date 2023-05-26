using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Media.Imaging;
using OLEDWindowsApplication.Models;
using Encoder = System.Text.Encoder;

namespace OLEDWindowsApplication;

using System;
using System.IO.Ports;
using System.Windows.Forms;

public class HardwareDataBridge
{	
    /// <summary> 	
    /// TCPListener to listen for incomming TCP connection 	
    /// requests. 	
    /// </summary> 	
    private TcpListener tcpListener; 
    /// <summary> 
    /// Background thread for TcpServer workload. 	
    /// </summary> 	
    private Thread tcpListenerThread;  	
    /// <summary> 	
    /// Create handle to connected tcp client. 	
    /// </summary> 	
    private TcpClient connectedTcpClient; 	

    public HardwareDataBridge()
    {
        // Start TcpServer background thread 		
        tcpListenerThread = new Thread (RecvKey); 		
        tcpListenerThread.IsBackground = true; 		
        tcpListenerThread.Start(); 	
    }

    public void WriteKey(Key i)
    {		
        if (connectedTcpClient == null) {             
            return;         
        }  		
		
        try { 			
            // Get a stream object for writing. 			
            NetworkStream stream = connectedTcpClient.GetStream(); 			
            if (stream.CanWrite)
            {
                //byte[] csString = Encoding.ASCII.GetBytes(i.CSString);
                //stream.Write(csString, 0, csString.Length);
                
                PiImage image = new PiImage(i.GetBitmap());

                // WriteLine byte array to socketConnection stream. 
                stream.Write(image.image, 0, image.image.Length);               
            }       
        } 		
        catch (SocketException socketException) {             
            Debug.WriteLine("Socket exception: " + socketException);         
        } 	
    }
    
    private static ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        int j;
        ImageCodecInfo[] encoders;
        encoders = ImageCodecInfo.GetImageEncoders();
        for(j = 0; j < encoders.Length; ++j)
        {
            if(encoders[j].MimeType == mimeType)
                return encoders[j];
        }
        return null;
    }

    private void RecvKey()
    {
        try
        {
            // Create listener port 8052. 			
            tcpListener = new TcpListener(IPAddress.Any, 8052);
            tcpListener.Start();
            Debug.WriteLine("Server is listening");
            Byte[] bytes = new Byte[1024];
            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    // Get a stream object for reading 					
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        int length;
                        // Read incoming stream into byte array. 						
                        while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            var incomingData = new byte[length];
                            Array.Copy(bytes, 0, incomingData, 0, length);
                            // Convert byte array to string message. 							
                            string clientMessage = Encoding.ASCII.GetString(incomingData);
                            Debug.WriteLine("Client message received as: " + clientMessage);
                            if (clientMessage.Length == 1)
                            {
                                SendKeys.SendWait("{" + clientMessage + "}");
                            }
                        }
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.WriteLine("SocketException " + socketException.ToString());
        }
    }
}