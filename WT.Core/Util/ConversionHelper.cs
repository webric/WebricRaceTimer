using System;
using System.Data;
using System.Text;

namespace IPS.Core.Util
{
    public class ConversionHelper
    {
        private static String _DefaultString = String.Empty;
        private static Guid _DefaultGuid = Guid.Empty;
        private static Byte[] _DefaultByte = { };
        private static int _DefaultInt = int.MinValue;
        private static decimal _DefaultDecimal = decimal.MinValue;
        private static float _DefaultFloat = float.MinValue;
        private static DateTime _DefaultDateTime = DateTime.MinValue;
        private static double _DefaultDouble = double.MinValue;
        private static bool notEmpty(object value)
        {
            if (value == null)
                return false;

            String test = String.Format("{0}", value);

            bool empty = String.IsNullOrEmpty(test)
                || String.IsNullOrEmpty(test.Trim())
                || Guid.Empty.Equals(value)
                || int.MinValue.Equals(value)
                || decimal.MinValue.Equals(value)
                || float.MinValue.Equals(value)
                || DateTime.MinValue.Equals(value)
                || double.MinValue.Equals(value);

            return !empty;
        }
        public static String toStr(Object value)
        {
            return toStr(value, _DefaultString);
        }
        public static Byte[] toByte(Object value)
        {
            return toByte(value, _DefaultByte);
        }
        public static Byte[] toByte(Object value, Byte[] defaultValue)
        {
            byte[] ret = defaultValue;
            if (notEmpty(value))
            {
                try
                {
                    ret = (byte[])value;
                }
                catch (Exception e)
                {
                    HandleException(String.Format("Error when converting byte value=\'{0}\'\n\n", value), e);
                }
            }
            return ret;
        }
        public static Guid toGuid(Object value)
        {
            return toGuid(value, _DefaultGuid);
        }
        public static Guid toGuid(Object value, Guid defaultValue)
        {
            Guid ret = defaultValue;
            if (notEmpty(value))
            {
                try
                {
                    ret = new Guid(toStr(value));
                }
                catch (Exception e)
                {
                    HandleException(String.Format("Error when converting guid value=\'{0}\'\n\n", value), e);
                }
            }
            return ret;
        }
        public static String toStr(Object value, String defaultValue)
        {
            System.String ret = defaultValue;
            if (notEmpty(value))
            {
                try
                {
                    ret = value.ToString();
                }
                catch (Exception e)
                {
                    HandleException(String.Format("Error when converting string value=\'{0}\'\n\n", value), e);
                }
            }
            return ret;
        }
        public static String toStr(Boolean value)
        {
            if (value)
                return Boolean.TrueString;
            else
                return Boolean.FalseString;
        }
        public static String toStr(DateTime value)
        {
            return toStr(value, String.Empty);
        }
        public static String toStr(DateTime value, String format)
        {
            string ret = _DefaultString;

            if (notEmpty(value) && !isMinDateTime(value))
            {
                try
                {
                    ret = value.ToString(format);
                }
                catch (Exception e)
                {
                    HandleException(String.Format("Error when converting DateTime value=\'{0}\' using format '{1}'\n\n", value, format), e);
                }
            }
            return ret;
        }
        public static String toStr(double value, int ExactDecimals)
        {
            string ret = _DefaultString;

            if (notEmpty(value))
            {
                try
                {
                    String decString = String.Empty.PadLeft(ExactDecimals, '0');
                    String parseString = "{0:0." + decString + "}";
                    ret = String.Format(parseString, value);
                }
                catch (Exception e)
                {
                    HandleException(String.Format("Error when converting double value=\'{0}\' using ExactDecimals= '{1}'\n\n", value, ExactDecimals), e);
                }
            }
            return ret;
        }
        public static int toInt(Object value)
        {
            return toInt(value, _DefaultInt);
        }
        public static int toInt(Object value, int defaultValue)
        {
            int ret = defaultValue;
            if (notEmpty(value))
            {
                try
                {
                    ret = Convert.ToInt32(value);
                }
                catch
                {
                    try
                    {
                        ret = Convert.ToInt32(value.ToString().Replace(',', '.'));
                    }
                    catch (Exception ex)
                    {
                        HandleException(String.Format("Error when converting int value=\'{0}\'\n\n", value), ex);
                    }
                }
            }
            return ret;
        }
        public static decimal toDecimal(Object value)
        {
            return toDecimal(value, _DefaultDecimal);
        }
        public static float toFloat(Object value)
        {
            return toFloat(value, _DefaultFloat);
        }
        public static float toFloat(Object value, float defaultValue)
        {
            float ret = defaultValue;
            if (notEmpty(value))
            {
                try
                {
                    ret = float.Parse(ConversionHelper.toStr(value));
                }
                catch (Exception e)
                {
                    HandleException(String.Format("Error when converting float value=\'{0}\'\n\n", value), e);
                }
            }
            return ret;
        }
        public static decimal toDecimal(Object value, decimal defaultValue)
        {
            decimal ret = defaultValue;
            if (notEmpty(value))
            {
                try
                {
                    ret = Convert.ToDecimal(value);
                }
                catch (Exception e)
                {
                    HandleException(String.Format("Error when converting decimal value=\'{0}\'\n\n", value), e);
                }
            }
            return ret;
        }
        public static double toDouble(Object value)
        {
            return toDouble(value, _DefaultDouble);
        }
        public static double toDouble(Object value, double defaultValue)
        {
            double ret = defaultValue;
            if (notEmpty(value))
            {
                try
                {
                    ret = Convert.ToDouble(value);
                }
                catch (Exception e)
                {
                    HandleException(String.Format("Error when converting double value=\'{0}\'\n\n", value), e);
                }
            }
            return ret;
        }
        public static bool toBool(Object value)
        {
            return toBool(value, false);
        }
        public static bool toBool(Object value, bool defaultValue)
        {
            bool ret = defaultValue;
            if (notEmpty(value))
            {
                try
                {
                    ret = Convert.ToBoolean(value);
                }
                catch (Exception e)
                {
                    HandleException(String.Format("Error when converting boolean value=\'{0}\'\n\n", value), e);
                }
            }
            return ret;
        }
        public static DateTime toDateTime(Object value, String format)
        {
            DateTime dt = _DefaultDateTime;
            if (notEmpty(value))
            {
                try
                {
                    dt = DateTime.ParseExact(value.ToString(), format, null);
                }
                catch (Exception e)
                {
                    HandleException(String.Format("Error when converting datetime value=\'{0}\'\n\n", value), e);
                }
            }
            return dt;
        }
        public static String toDateTimeStringYYYYMMD(DateTime value)
        {
            var dts = "";
            if (notEmpty(value))
            {
                try
                { dts = value.ToString("yyyy-MM-dd"); }
                catch (Exception e)
                { HandleException(String.Format("Error when converting datetime value=\'{0}\'\n\n", value), e); }
            }
            return dts;
        }
        public static String toDateTimeStringYYMMD(DateTime value)
        {
            var dts = "";
            if (notEmpty(value))
            {
                try
                { dts = value.ToString("yy-MM-dd"); }
                catch (Exception e)
                { HandleException(String.Format("Error when converting datetime value=\'{0}\'\n\n", value), e); }
            }
            return dts;
        }
        public static bool toDateTimeTry(Object value, out DateTime result)
        {
            result = _DefaultDateTime;
            bool succes = false;
            if (notEmpty(value))
            {
                succes = DateTime.TryParse(value.ToString(), out result);
            }
            return succes;
        }
        public static DateTime toDateTime(Object value)
        {
            return toDateTime(value, _DefaultDateTime);
        }
        public static DateTime toDateTime(Object value, DateTime defaultValue)
        {
            DateTime ret = defaultValue;
            if (notEmpty(value))
            {
                try
                {
                    ret = Convert.ToDateTime(value);
                }
                catch (Exception e)
                {
                    HandleException(String.Format("Error when converting datetime value=\'{0}\'\n\n", value), e);
                }
            }
            return ret;
        }
        public static Boolean isMinDateTime(DateTime dt)
        {
            return (dt > _DefaultDateTime ? false : true) || (dt.Ticks == 0);
        }
        private static void HandleException(String Message, Exception e)
        {
            LoggHelper.Error(Message, LoggHelper.State.ConvertError, "", e);
        }
        public static string toCurrency(object value)
        {
            return toCurrency(value, _DefaultString, 3);
        }
        public static string toCurrency(object value, string defaultValue)
        {
            return toCurrency(value, defaultValue, 3);
        }
        private static string toCurrency(object value, string defaultValue, int skipFrames)
        {
            string ret = defaultValue;
            if (((value != null) & !string.Empty.Equals(value)))
            {
                try
                {
                    ret = decimal.Parse(Convert.ToString(value)).ToString("N2");
                }
                catch
                {
                    ret = defaultValue;
                }
            }
            return ret.Trim();
        }







        /// <summary>
        /// Konverterar SqlDBType till "vanlig" .Net type
        /// </summary>
        //public static String GetCTypeBySqlDbType(string type, bool isNullable, bool useNullable, bool LanguageIsC)
        //{
        //    return GetCTypeBySqlDbType(SQLHelper.ConvertStringToSQLDbType(type), isNullable, useNullable, LanguageIsC);
        //}
        public static String GetCTypeBySqlDbType(SqlDbType sqlType, bool isNullable, bool useNullable, bool LanguageIsC)
        {
            switch (sqlType)
            {
                case SqlDbType.BigInt:
                    if (isNullable)
                    {
                        if (useNullable)
                            if (LanguageIsC)
                                return typeof(long?).ToString();
                            else
                                return "Nullable(Of Long)";
                        else
                            return typeof(long).ToString();
                    }
                    else
                        return typeof(long).ToString();
                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return typeof(byte[]).ToString();

                case SqlDbType.Bit:
                    return typeof(bool).ToString();

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                    return typeof(string).ToString();

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                    return typeof(DateTime).ToString();

                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return typeof(decimal).ToString();

                case SqlDbType.Float:
                    return typeof(double).ToString();

                case SqlDbType.Int:
                    if (isNullable)
                    {
                        if (useNullable)
                            if (LanguageIsC)
                                return typeof(int?).ToString();
                            else
                                return "Nullable(Of Integer)";
                        else
                            return typeof(int).ToString();
                    }
                    else
                        return typeof(int).ToString();

                case SqlDbType.Real:
                    return typeof(float).ToString();

                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid).ToString();

                case SqlDbType.SmallInt:
                    return typeof(short).ToString();

                case SqlDbType.TinyInt:
                    return typeof(byte).ToString();

                case SqlDbType.Variant:
                case SqlDbType.Udt:
                    return typeof(object).ToString();

                case SqlDbType.Structured:
                    return typeof(DataTable).ToString();

                case SqlDbType.DateTimeOffset:
                    return typeof(DateTimeOffset).ToString();

                default:
                    throw new ArgumentOutOfRangeException("sqlType");
            }
        }
        /// <summary>
        /// Ej testad
        /// </summary>
        public static string[] StringToArray(string input, char splitChar)
        {
            string[] work = input.Split(splitChar);
            string output = string.Empty;

            if (work.Length > 1)
            {
                for (int i = 0; i < work.Length; i++)
                {
                    work[i] = work[i].TrimEnd(' ').TrimStart(' ');
                    if (work[i].Length != 0)
                        output += work[i] + splitChar;
                }
            }

            return output.Substring(0, output.Length - 1).Split(splitChar);
        }
        /// <summary>
        /// Ej testad
        /// </summary>
        public static string ArrayToString(string[] input, string splitChar)
        {
            string output = string.Empty;

            foreach (string t in input)
            {
                output = splitChar + t + splitChar;
            }

            output = output.Replace(splitChar + splitChar, splitChar);

            return output;
        }
        /// <summary>
        /// Konvertera en string till en encodad string
        /// http://www.eggheadcafe.com/community/aspnet/18/28998/msmq--mqbridge--ibm-mq.aspx
        /// </summary>
        public static string ConvertStringToEncoding(string inputString, Encoding enc)
        {
            Encoding cod;
            switch (enc.WebName)
            {
                case "ASCII": cod = Encoding.GetEncoding("ASCII"); break;
                case "BigEndianUnicode": cod = Encoding.GetEncoding("BigEndianUnicode"); break;
                case "Default": cod = Encoding.GetEncoding("Default"); break;
                case "Unicode": cod = Encoding.GetEncoding("Unicode"); break;
                case "UTF-32": cod = Encoding.GetEncoding("UTF-32"); break;
                case "UTF-7": cod = Encoding.GetEncoding("UTF-7"); break;
                case "UTF-8": cod = Encoding.GetEncoding("UTF-8"); break;
                default: cod = Encoding.GetEncoding("Default"); break;
            }

            // Create two different encodings.
            Encoding ascii = Encoding.ASCII;
            byte[] ASCIIBytes = ascii.GetBytes(inputString);
            // Perform the conversion from one encoding to the other.			 
            byte[] EBCDIC = Encoding.Convert(ascii, cod, ASCIIBytes);
            return ascii.GetString(EBCDIC);
        }
        #region Temperatur
        public static double CelsiusToFahrenheit(double Celsius)
        {
            return (Celsius * 9) / 5 + 32;
        }
        public static double CelsiusToKelvin(double Celsius)
        {
            return Celsius + 273.15;
        }
        public static double CelsiusToRankine(double Celsius)
        {
            return (5 / 9) * Celsius + 491.69;
        }
        public static double CelsiusToReaumur(double Celsius)
        {
            return (Celsius * 4) / 5;
        }
        public static double CelsiusToRomer(double Celsius)
        {
            return Celsius * 21 / 40 + 7.5;
        }
        public static double CelsiusToDelisle(double Celsius)
        {
            return (100 - Celsius) * 3 / 2;
        }
        public static double CelsiusToNewton(double Celsius)
        {
            return Celsius * 33 / 100;
        }
        public static double FahrenheitToCelsius(double fahrenheit)
        {
            return (fahrenheit - 32) * 5 / 9;
        }
        public static double FahrenheitToKelvin(double fahrenheit)
        {
            return (fahrenheit + 459.67) * 5 / 9;
        }
        public static double FahrenheitToRankine(double Fahrenheit)
        {
            return Fahrenheit + 459.67;
        }
        public static double FahrenheitToReaumur(double Fahrenheit)
        {
            return (Fahrenheit - 32) * 4 / 9;
        }
        public static double FahrenheitToRomer(double Fahrenheit)
        {
            return (Fahrenheit - 32) * 7 / 24 + 7.5;
        }
        public static double FahrenheitToDelisle(double Fahrenheit)
        {
            return (121 - Fahrenheit) * 5 / 6; ;
        }
        public static double FahrenheitToNewton(double Fahrenheit)
        {
            return (Fahrenheit - 32) * 11 / 60;
        }
        public static double KelvinToCelsius(double Kelvin)
        {
            return Kelvin - 273.15;
        }
        public static double KelvinToFahrenheit(double Kelvin)
        {
            return (KelvinToCelsius(Kelvin) * 9) / 5 + 32;
        }
        public static double KelvinToRankine(double Kelvin)
        {
            return (9 / 5) * Kelvin + 764.84;
        }
        public static double RankineToCelsius(double Rankine)
        {
            return (5 / 9) * (Rankine - 491.69);
        }
        public static double RankineToFahrenheit(double Rankine)
        {
            return Rankine - 459.69;
        }
        public static double RankineToKelvin(double Rankine)
        {
            return (9 / 5) * (Rankine - 764.84);
        }
        public static double ReaumurToFahrenheit(double reaumur)
        {
            return (reaumur - 7.5) * 24 / 7 + 32;
        }
        public static double ReaumurToCelsius(double reaumur)
        {
            return 1.25 * reaumur;
        }
        public static double RomerToFahrenheit(double Romer)
        {
            return (Romer - 7.5) * 24 / 7 + 32;
        }
        public static double RomerToCelsius(double Romer)
        {
            return (Romer - 7.5) * 40 / 21;
        }
        public static double DelisleToFahrenheit(double Delisle)
        {
            return 121 - Delisle * 6 / 5;
        }
        public static double DelisleToCelsius(double Delisle)
        {
            return 100 - Delisle * 2 / 3;
        }
        public static double NewtonToFahrenheit(double Newton)
        {
            return Newton * 60 / 11 + 32;
        }
        public static double NewtonToCelsius(double Newton)
        {
            return Newton * 100 / 33;
        }
        #endregion
        #region Längd
        public static double MeterToMile(double Meter)
        {
            return Meter / 1609.344;
        }
        public static double MeterToYard(double Meter)
        {
            return Meter / 0.91;
        }
        public static double MeterToFot(double Meter)
        {
            return Meter / (2.474175 * 100);
        }
        public static double MeterToNautiskmil(double Meter)
        {
            return Meter / 1852;
        }
        public static double MeterToFamn(double Meter)
        {
            return Meter / 1.83;
        }
        public static double MeterToOrgyja(double Meter)
        {
            return Meter / 1.85;
        }
        public static double MeterToStadion(double Meter)
        {
            return Meter / 185;
        }
        public static double MeterToMilion(double Meter)
        {
            return Meter / 1478;
        }
        public static double MeterToTum(double Meter)
        {
            return Meter / 0.03;
        }
        public static double MeterToAln(double Meter)
        {
            return Meter / (59.4 * 100);
        }
        public static double MeterToBiblicalNTAln(double Meter)
        {
            return Meter / (550 * 100);
        }
        public static double MeterToBiblicalGTAln(double Meter)
        {
            return Meter / (445 * 100);
        }
        public static double MeterToFinger(double Meter)
        {
            return Meter / 0.0019;
        }
        public static double MeterToHandsbredd(double Meter)
        {
            return Meter / 0.0076;
        }
        public static double MeterToKvarter(double Meter)
        {
            return Meter / 0.0230;
        }
        public static double FingerToMeter(double Finger)
        {
            return Finger * 0.019;
        }
        public static double MileToMeter(double Mile)
        {
            return Mile * 1609.344;
        }
        public static double YardToMeter(double Yard)
        {
            return Yard * 0.91;
        }
        public static double FotToMeter(double Fot)
        {
            return Fot * (2.474175 * 100);
        }
        public static double NautiskmilToMeter(double Nautiskmil)
        {
            // Distansminut, nautiskmil, sjömil
            return Nautiskmil * 1852;
        }
        public static double FamnToMeter(double Famn)
        {
            return Famn * 1.83;
        }
        public static double OrgyjaToMeter(double Orgyja)
        {
            return Orgyja * 1.85;
        }
        public static double StadionToMeter(double Stadion)
        {
            return Stadion * 185;
        }
        public static double MilionToMeter(double Milion)
        {
            return Milion * 1478;
        }
        public static double TumToMeter(double Tum)
        {
            return Tum * 0.03;
        }
        public static double HandsbreddToMeter(double Handsbredd)
        {
            return Handsbredd * 0.076;
        }
        public static double KvarterToMeter(double Kvarter)
        {
            return Kvarter * 0.230;
        }
        public static double AlnToMeter(double Aln)
        {
            return Aln * (59.4 * 100);
        }
        public static double BiblicalNTAlnToMeter(double Aln)
        {
            return Aln * (550 * 100);
        }
        public static double BiblicalGTAlnToMeter(double Aln)
        {
            return Aln * (445 * 100);
        }
        #endregion
        #region Rymd
        public static double LiterToKab(double Liter)
        {
            return Liter / 1.2;
        }
        public static double LiterToHin(double Liter)
        {
            return Liter / 3.66;
        }
        public static double LiterToBat(double Liter)
        {
            return Liter / 22;
        }
        public static double LiterToHomer(double Liter)
        {
            return Liter / 220;
        }
        public static double LiterToLog(double Liter)
        {
            return Liter * 03;
        }
        public static double LiterToGomer(double Liter)
        {
            return Liter / 2.2;
        }
        public static double LiterToSea(double Liter)
        {
            return Liter / 7.3;
        }
        public static double LiterToEfa(double Liter)
        {
            return Liter / 22;
        }
        public static double KabToLiter(double Kab)
        {
            return Kab * 1.2;
        }
        public static double HinToLiter(double Hin)
        {
            return Hin * 3.66;
        }
        public static double BatToLiter(double Bat)
        {
            return Bat * 22;
        }
        public static double HomerToLiter(double Homer)
        {
            return Homer * 220;
        }
        public static double LogToLiter(double Log)
        {
            return Log / 0.3;
        }
        public static double GomerToLiter(double Gomer)
        {
            return Gomer * 2.2;
        }
        public static double SeaToLiter(double Sea)
        {
            return Sea * 7.3;
        }
        public static double EfaToLiter(double Efa)
        {
            return Efa * 22;
        }
        #endregion
        #region Vikt
        public static double GramToSikel(double Gram)
        {
            return (Gram / 91.4) * 8;
        }
        public static double GramToMina(double Gram)
        {
            return (Gram / 333) / (2 / 3);
        }
        public static double SikelToGram(double Sikel)
        {
            return 91.4 * (Sikel / 8);
        }
        public static double MinaToGram(double Mina)
        {
            return 333 * (2 / 3) * Mina;
        }
        #endregion
        //http://www.convertworld.com/sv/langd/Fot.html
    }
}
