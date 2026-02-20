using System;
using System.Threading;

namespace PatternsDemo2
{
    // =========================
    // ===== ADAPTER ===========
    // =========================

    public interface IXmlProcessor
    {
        void ProcessXml(string xmlData);
    }

    public class ThirdPartyJsonProcessor
    {
        public void HandleJson(string jsonData)
        {
            Console.WriteLine($"Processing JSON data: {jsonData}");
        }
    }

    public class JsonToXmlAdapter : IXmlProcessor
    {
        private ThirdPartyJsonProcessor _jsonProcessor;

        public JsonToXmlAdapter(ThirdPartyJsonProcessor jsonProcessor)
        {
            _jsonProcessor = jsonProcessor;
        }

        public void ProcessXml(string xmlData)
        {
            Console.WriteLine("Converting XML to JSON...");
            string jsonData = "{ \"convertedFromXml\": \"" + xmlData + "\" }";
            _jsonProcessor.HandleJson(jsonData);
        }
    }

    public class ClientApp
    {
        public void Run(IXmlProcessor processor)
        {
            processor.ProcessXml("<data><item>1</item></data>");
        }
    }

    // =========================
    // ===== FACADE ============
    // =========================

    public class Amplifier
    {
        public void TurnOn() => Console.WriteLine("Amplifier: On");
        public void SetVolume(int volume) => Console.WriteLine($"Amplifier: Volume {volume}");
        public void TurnOff() => Console.WriteLine("Amplifier: Off");
    }

    public class DvdPlayer
    {
        public void TurnOn() => Console.WriteLine("DVD Player: On");
        public void PlayMovie(string movie) => Console.WriteLine($"DVD Player: Playing {movie}");
        public void TurnOff() => Console.WriteLine("DVD Player: Off");
    }

    public class Projector
    {
        public void TurnOn() => Console.WriteLine("Projector: On");
        public void SetInput(string input) => Console.WriteLine($"Projector: Input {input}");
        public void TurnOff() => Console.WriteLine("Projector: Off");
    }

    public class HomeTheaterFacade
    {
        private Amplifier _amp;
        private DvdPlayer _dvd;
        private Projector _proj;

        public HomeTheaterFacade(Amplifier amp, DvdPlayer dvd, Projector proj)
        {
            _amp = amp;
            _dvd = dvd;
            _proj = proj;
        }

        public void WatchMovie(string movie)
        {
            Console.WriteLine("Get ready to watch a movie...");
            _proj.TurnOn();
            _proj.SetInput("DVD");
            _amp.TurnOn();
            _amp.SetVolume(10);
            _dvd.TurnOn();
            _dvd.PlayMovie(movie);
        }

        public void EndMovie()
        {
            Console.WriteLine("Shutting down home theater...");
            _dvd.TurnOff();
            _amp.TurnOff();
            _proj.TurnOff();
        }
    }

    // =========================
    // ===== PROXY =============
    // =========================

    public interface IImage
    {
        void Display();
    }

    public class RealImage : IImage
    {
        private string _filename;

        public RealImage(string filename)
        {
            _filename = filename;
            LoadImageFromDisk();
        }

        private void LoadImageFromDisk()
        {
            Console.WriteLine($"Loading image: {_filename}");
            Thread.Sleep(1000);
        }

        public void Display()
        {
            Console.WriteLine($"Displaying image: {_filename}");
        }
    }

    public class ProxyImage : IImage
    {
        private RealImage _realImage;
        private string _filename;

        public ProxyImage(string filename)
        {
            _filename = filename;
        }

        public void Display()
        {
            if (_realImage == null)
            {
                _realImage = new RealImage(_filename);
            }
            _realImage.Display();
        }
    }

    // =========================
    // ========= MAIN ==========
    // =========================

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ADAPTER ===");
            ClientApp app = new ClientApp();
            var jsonProcessor = new ThirdPartyJsonProcessor();
            IXmlProcessor adapter = new JsonToXmlAdapter(jsonProcessor);
            app.Run(adapter);

            Console.WriteLine();

            Console.WriteLine("=== FACADE ===");
            var amp = new Amplifier();
            var dvd = new DvdPlayer();
            var proj = new Projector();
            var homeTheater = new HomeTheaterFacade(amp, dvd, proj);

            homeTheater.WatchMovie("The Matrix");
            homeTheater.EndMovie();

            Console.WriteLine();

            Console.WriteLine("=== PROXY ===");
            IImage image1 = new ProxyImage("photo1.jpg");

            Console.WriteLine("First call:");
            image1.Display();

            Console.WriteLine("Second call:");
            image1.Display();
        }
    }
}
