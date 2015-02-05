using System;
using System.Collections.Concurrent;
using System.Threading;
public class ThreadSafe
{
    
    static BlockingCollection<string> bloqueur;
    static int limit = 30;
    static int vitesseConsommateur = 5000;
    static int vitesseProducteur = 300;
    public static Random random = new Random();
    public static void Main()
    {
        bloqueur = new BlockingCollection<string>(limit);
        new Thread(() => LancerProducteur()).Start();
        new Thread(() => LancerConsommateur()).Start();
    }


    public static void Producteur()
    {

   

            try
            {
                bloqueur.Add("toto");
                
                Console.WriteLine("PRODUCTEUR : " + bloqueur.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
                
                 
        

        
       

    }

    public static void Consommateur(int nb)
    {
        bool end = true;
        for (int compteurConsommateur = 0; compteurConsommateur < nb; compteurConsommateur++)
        {
            string valeurDelete = "";
            try
            {

                valeurDelete = bloqueur.Take();
                
                if (!end)
                {
                    Console.WriteLine("Out of stock");
                    break;
                }
                Console.WriteLine("Nouveau consommateur : " + (compteurConsommateur + 1) + " / " + nb);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }
      
    }

    public static void LancerConsommateur()
    {
        while (true)
        {
            System.Threading.Thread.Sleep(vitesseConsommateur);
            Consommateur(random.Next(1,10));
        }
    }
    private static void LancerProducteur()
    {
        while (true)
        {
            Producteur();
            System.Threading.Thread.Sleep(vitesseProducteur);
        }
    }

}