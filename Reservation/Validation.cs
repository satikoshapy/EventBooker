using System;

namespace Reservation
{
    public static class Validation
    {
        
        // ToDo: validate the national insurance number. Throw a validationException if the
        //       national insurance number is not correct.
        public static bool Validate(string nationalInsuranceNumber)
        {
            if (nationalInsuranceNumber != null && nationalInsuranceNumber.Length == 11)
            {

                string firstNums = nationalInsuranceNumber.Substring(0, 9);
                string lastTwoNums = nationalInsuranceNumber.Substring(9);
                int controlNum = Convert.ToInt32(lastTwoNums);

                int result = Math.Abs(Convert.ToInt32(lastTwoNums) / Convert.ToInt32(firstNums));
                if (Convert.ToInt32(lastTwoNums) - result == controlNum)
                {
                    return true;
                }
                else
                {
                    
                    throw new ValidationException("Not valid");
                    
                }
                
            }
            else
            {
                
                throw new ValidationException("Please enter 11 digit numbers");
                
            }
            return false;
        }
    }
}
