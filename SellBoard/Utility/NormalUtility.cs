using DAL.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SellBoard.Utility
{
    public class NormalUtility
    {
        private UnitOfWork db = new UnitOfWork();
        public DateTime ConvertStringToDate(string date)
        {
            try
            {
                DateTime myDate = DateTime.ParseExact(date, "dddd dd MMMM yyyy - HH:mm",
                                                       CultureInfo.InvariantCulture);
                return myDate;
            }
            catch (Exception e)
            {

                throw e.InnerException;
            }
        }
        /// <summary>
        /// convert string fotmat to date format that string has only date value without time 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime ConvertStringToDateOnly(string date)
        {
            try
            {
                DateTime myDate = DateTime.ParseExact(date, "dddd dd MMMM yyyy",
                                                       CultureInfo.InvariantCulture);
                return myDate;
            }
            catch (Exception e)
            {

                throw e.InnerException;
            }
        }
        public string ConvertDateToString(DateTime date)
        {
            try
            {
                return date.ToString("dddd dd MMMM yyyy - HH:mm");
            }
            catch (Exception e)
            {

                throw e.InnerException;
            }
        }
        public string ConvertDateToStringOnlydate(DateTime date)
        {
            try
            {
                return date.ToString("dddd dd MMMM yyyy");
            }
            catch (Exception e)
            {

                throw e.InnerException;
            }
        }
        public int CompareDate(DateTime Start, DateTime End)
        {
            if (DateTime.Compare(Start, End) > 0)
                return 1;   //first is grater than seccond
            if (DateTime.Compare(Start, End) < 0)
                return 2;  //seccond is grater than first
            return 0;      // both are equal

        }
        public string StringArrayToString(string[] input)
        {
            string output=null;
            try
            {
                if (input!=null && input.Length > 0)
                {
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (i == input.Length - 1)
                        {
                            output += input[i];
                        }
                        else
                        {
                            output += input[i] + ",";
                        }
                        
                    }
                }
                return output;
            }
            catch (Exception e)
            {

                throw e.InnerException;
            }
        }
        public string[] StringToarrayString( string input)
        {
            string[] Output = new string[] { "" };
            try
            {
                 if(input!=null&& !string.IsNullOrEmpty(input))
                {
                    Output = input.Split(',');
                }
                return Output;
            }
            catch (Exception e)
            {

                throw e.InnerException;
            }
        }
        //public Tbl_Setting Get_Setting()
        //{
        //    try
        //    {
        //        var setting = db.SettingRepository.Get().FirstOrDefault();
        //        return setting;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}
        public DateTime ConvertStaringToDate(string date,string format)
        {
            DateTime dt = DateTime.ParseExact(date, format,
                                                      CultureInfo.InvariantCulture);
            return dt;
        }
    }
}
