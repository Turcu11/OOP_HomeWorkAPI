namespace WebAPIexercitii.Modells
{
    public class Product
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; }
        public int[] Rating { get; set; }
        public DateTime CreatedOn { get; set; }

        public double GetAverageRating()
        {
            double average = 0;
            for (int i = 0; i < Rating.Length; i++)
            {
                average += Rating[i];
            }
            average /= (Rating.Length);
            Console.WriteLine(average.ToString());

            return average;
        }

        /// <summary>
        /// This will return the higher rated product betwen the 2
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public Product CompareRating(Product p1,  Product p2)
        {
            if(p1.GetAverageRating() > p2.GetAverageRating())
            {
                return p1;
            }
            else return p2;
        }
    }
}