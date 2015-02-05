using System;
using System.Collections.Concurrent;
using System.Threading;
public class ThreadSafe
{
    public static ConcurrentQueue<string> maListe = new ConcurrentQueue<string>();
    static int limit = 200;
    static int vitesseConsommateur = 800;
    static int vitesseProducteur = 200;
    public static Random random = new Random();
    public static void Main()
    {
            new Thread(() => LancerProducteur()).Start();
            new Thread(() =>LancerConsommateur()).Start();
    }


    public static void Producteur(int nb)
    {
        int produit = 0;
        for (int compteurProduction = 0; compteurProduction < nb; compteurProduction++)
        {
            if (maListe.Count >= limit)
            {

            }
            else
            {
                maListe.Enqueue(random.Next(100).ToString());
                produit++;
            }
        }
        Console.WriteLine("PRODUCTEUR : " + produit);

    }

    public static void Consommateur(int nb)
    {
        int consommé = 0;
        for (int compteurConsommateur = 0; compteurConsommateur < nb; compteurConsommateur++)
        {
            string valeurDelete = "";
            if (!maListe.IsEmpty)
            {
                consommé++;
                maListe.TryDequeue(out valeurDelete);
            }
        }
        Console.WriteLine("CONSOMME : " + consommé);
    }

    public static void LancerConsommateur()
    {
        while (true)
        {
            System.Threading.Thread.Sleep(vitesseConsommateur);
            Consommateur(random.Next(100));
        }
    }
    private static void LancerProducteur()
    {
        while (true)
        {
            Producteur(random.Next(100));
            System.Threading.Thread.Sleep(vitesseProducteur);
        }    
    }


}