using System;

namespace EstructurasDatos.Logic
{
    public class Time
    {
        // Campos privados
        private int _hour;
        private int _minute;
        private int _second;
        private int _millisecond;

        // Propiedades
        public int Hour 
        {
            get { return _hour; } 
            set { _hour = value; } 
        }

        public int Minute
        {
            get { return _minute; } 
            set { _minute = value; } 
        }

        public int Second
        {
            get { return _second; } 
            set { _second = value; } 
        }

        public int Millisecond
        {
            get { return _millisecond; } 
            set { _millisecond = value; } 
        }

        // Constructor sin parámetros
        public Time() : this(0, 0, 0, 0) { }

        // Constructor con hora solamente
        public Time(int hour) : this(hour, 0, 0, 0) { }

        // Constructor con hora y minutos
        public Time(int hour, int minute) : this(hour, minute, 0, 0) { }

        // Constructor con hora, minutos y segundos
        public Time(int hour, int minute, int second) : this(hour, minute, second, 0) { }


        // Constructor completo
        public Time(int hour, int minute, int second, int millisecond)
        {
            _hour = hour;
            _minute = minute;
            _second = second;
            _millisecond = millisecond;
        }

        // Método para validar si la hora es válida
        public bool ValidHour()
        {
            return _hour >= 0 && _hour <= 23 && _minute >= 0 && _minute <= 59
                && _second >= 0 && _second <= 59 && _millisecond >= 0 && _millisecond <= 999;
        }

        // Método ToString ara mostrar en formato 12 horas
        public override string ToString()
        {
            if (!ValidHour())
                throw new Exception($"The Hour: {_hour}, is not valid");

            int hora = _hour == 0 ? 0 : _hour > 12 ? _hour - 12 : _hour;
            if (_hour == 0) hora = 0; // Aquí para medianoche mostrar 00
            string ampm = _hour < 12 ? "AM" : "PM";
            return $"{hora:D2}:{_minute:D2}:{_second:D2}:{_millisecond:D3} {ampm}";
        }


        // Método para vonvertir a milisegundos desde medianoche
        public int ToMilliseconds()
        {
            if (!ValidHour()) return 0;
            return _hour * 3600000 + _minute * 60000 + _second * 1000 + _millisecond;
        }

        // Método para convertir a segundos desde medianoche
        public int ToSeconds()
        {
            if (!ValidHour()) return 0;
            return _hour * 3600 + _minute * 60 + _second;
        }

        // Método para convertir a minutos desde medianoche
        public int ToMinutes()
        {
            if (!ValidHour()) return 0;
            return _hour * 60 + _minute;
        }


        // Método para sumar dos tiempos
        public Time Add(Time other)
        {
            // Sumar milisegundos
            int ms = _millisecond + other._millisecond;
            int s = _second + other._second + ms / 1000;
            ms = ms % 1000;

            // Sumar segundos
            int m = _minute + other._minute + s / 60;
            s = s % 60;

            // Sumar minutos
            int h = _hour + other._hour + m / 60;
            m = m % 60;

            // Las horas se mantienen en el rango 0 - 23 para el día siguiente
            h = h % 24;

            return new Time(h, m, s, ms);
        }

        // // Método para verificar si la suma pasa al siguiente día
        public bool IsOtherDay(Time other)
        {
            int totalMs = _millisecond + other._millisecond;
            int totalS = _second + other._second + totalMs / 1000;
            int totalM = _minute + other._minute + totalS / 60;
            int totalH = _hour + other._hour + totalM / 60;

            return totalH > 23;
        }
    }
}
