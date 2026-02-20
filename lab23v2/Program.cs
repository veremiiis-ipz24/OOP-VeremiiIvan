using System;

namespace Lab23
{
    // BEFORE: violations
    class LightController { public void TurnOn() => Console.WriteLine("Light ON"); }
    class MusicPlayer { public void Play() => Console.WriteLine("Music playing"); }
    class SecurityAlarm { public void Arm() => Console.WriteLine("Alarm armed"); }

    class SmartHomeCentral_Bad
    {
        private LightController light = new LightController();
        private MusicPlayer music = new MusicPlayer();
        private SecurityAlarm alarm = new SecurityAlarm();

        public void ActivateAll()
        {
            light.TurnOn();
            music.Play();
            alarm.Arm();
        }
    }

    // AFTER: ISP + DIP

    interface ILight { void TurnOn(); }
    interface IMusic { void Play(); }
    interface IAlarm { void Arm(); }

    class LightDevice : ILight
    {
        public void TurnOn() => Console.WriteLine("Light ON");
    }

    class MusicDevice : IMusic
    {
        public void Play() => Console.WriteLine("Music playing");
    }

    class AlarmDevice : IAlarm
    {
        public void Arm() => Console.WriteLine("Alarm armed");
    }

    class SmartHomeCentral
    {
        private readonly ILight light;
        private readonly IMusic music;
        private readonly IAlarm alarm;

        public SmartHomeCentral(ILight l, IMusic m, IAlarm a)
        {
            light = l;
            music = m;
            alarm = a;
        }

        public void ActivateAll()
        {
            light.TurnOn();
            music.Play();
            alarm.Arm();
        }
    }

    class Program
    {
        static void Main()
        {
            Console.WriteLine("Bad version:");
            var bad = new SmartHomeCentral_Bad();
            bad.ActivateAll();

            Console.WriteLine("\nRefactored version:");
            var hub = new SmartHomeCentral(
                new LightDevice(),
                new MusicDevice(),
                new AlarmDevice());

            hub.ActivateAll();
        }
    }
}
