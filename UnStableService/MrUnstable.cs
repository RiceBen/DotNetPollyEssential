using System;

namespace UnStableService
{
    /// <summary>
    /// Mr. Unstable class
    /// </summary>
    public class MrUnstable
    {
        /// <summary>
        /// Raise random exception
        /// </summary>
        /// <returns></returns>
        public void RandomException()
        {
            Random random = new Random(DateTime.UtcNow.Second);

            var randomNumber = random.Next(0, 10);

            if (randomNumber % 3 == 0)
            {
                Console.WriteLine("random application exception");
                throw new ApplicationException("random application exception");
            }
            else if (randomNumber % 3 == 1)
            {
                Console.WriteLine("random null reference exception");
                throw new NullReferenceException("random null reference exception");
            }
            else
            {
                Console.WriteLine("random invalid operation exception");
                throw new InvalidOperationException("random invalid operation exception");
            }
        }

        public void SpecificException(Exception ex)
        {
            Console.WriteLine($"Execution SpecificException, {ex.GetType().Name}");
            throw ex;
        }
    }
}