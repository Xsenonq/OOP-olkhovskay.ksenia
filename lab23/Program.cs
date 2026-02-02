using System;
namespace Lab23
{
    class LightControllerBad
    {
        public void TurnOnLight()
        {
            Console.WriteLine("Світло увімкнено (Bad)");
        }
    }

    class MusicPlayerBad
    {
        public void PlayMusic()
        {
            Console.WriteLine("Музика грає (Bad)");
        }
    }

    class SecurityAlarmBad
    {
        public void ActivateAlarm()
        {
            Console.WriteLine("Сигналізація активована (Bad)");
        }
    }

    class SmartHomeCentralBad
    {
        private LightControllerBad _light;
        private MusicPlayerBad _music;
        private SecurityAlarmBad _alarm;

        public SmartHomeCentralBad()
        {
            _light = new LightControllerBad();
            _music = new MusicPlayerBad();
            _alarm = new SecurityAlarmBad();
        }

        public void StartEveningMode()
        {
             _light.TurnOnLight();
            _music.PlayMusic();
            _alarm.ActivateAlarm();
        }
    }

    interface ILight
    {
        void TurnOn();
    }

    interface IMusicPlayer
    {
        void Play();
    }

    interface ISecurityAlarm
    {
        void Activate();
    }
    class LightController : ILight
    {
        public void TurnOn()
        {
            Console.WriteLine("Світло увімкнено");
        }
    }

    class MusicPlayer : IMusicPlayer
    {
        public void Play()
        {
            Console.WriteLine("Музика грає");
        }
    }

    class SecurityAlarm : ISecurityAlarm
    {
        public void Activate()
        {
            Console.WriteLine("Сигналізація активована");
        }
    }
    class SmartHomeCentral
    {
        private readonly ILight _light;
        private readonly IMusicPlayer _music;
        private readonly ISecurityAlarm _alarm;

        public SmartHomeCentral(
            ILight light,
            IMusicPlayer music,
            ISecurityAlarm alarm)
        {
            _light = light;
            _music = music;
            _alarm = alarm;
        }

        public void StartEveningMode()
        {
            _light.TurnOn();
            _music.Play();
            _alarm.Activate();
        }
    }
    class Program
    {
        static void Main()
        {
            Console.WriteLine("ПОЧАТКОВА РЕАЛІЗАЦІЯ ");
            var badHome = new SmartHomeCentralBad();
            badHome.StartEveningMode();

            Console.WriteLine("\nПІСЛЯ РЕФАКТОРИНГУ");

            ILight light = new LightController();
            IMusicPlayer music = new MusicPlayer();
            ISecurityAlarm alarm = new SecurityAlarm();

            var goodHome = new SmartHomeCentral(light, music, alarm);
            goodHome.StartEveningMode();
        }
    }
}
