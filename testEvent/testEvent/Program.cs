using System;
using System.Threading;

namespace testEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            var video1 = new Video() { title = "Video 1" };

            var myVideo = new EncodingVideo();
            var mail = new MailService();
            var message = new MessageService();

            myVideo.VideoEncoded += mail.OnVideoEncoded;
            myVideo.VideoEncoded += message.OnVideoEncoded;

            myVideo.Encoding(video1);

        }
    }

    public class Video
    {
        public string title { get; set; }

    }

    public class VideoEventArgs : EventArgs
    {
        public Video Eventvideo { get; set; }
    }


    public class EncodingVideo
    {
        // 1- Define a delegate for sub and publishers
        // 2- Define an event based on the delegate
        // 3- Rasie the event

        public delegate void VideoEncodingHandler(object sender, VideoEventArgs e);

        public event VideoEncodingHandler VideoEncoded;

        public void Encoding(Video video)
        {
            //Encoding process....

            System.Console.WriteLine("Encoding is done!");
            Thread.Sleep(3000);

            OnVideoEncoded(video);

        }

        protected virtual void OnVideoEncoded(Video video)
        {
            if (VideoEncoded != null)
            {
                VideoEncoded(this, new VideoEventArgs { Eventvideo = video });
            }

        }

    }

    public class MessageService
    {
        public void OnVideoEncoded(object sender, VideoEventArgs e)
        {
            Console.WriteLine("Message service started! " + e.Eventvideo.title);
        }

    }

    public class MailService
    {
        public void OnVideoEncoded(object sender, VideoEventArgs args)
        {
            Console.WriteLine("Mail service started! " + args.Eventvideo.title);
        }
    }










}
