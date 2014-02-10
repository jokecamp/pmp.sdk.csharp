using System;
using System.IO;
using System.Linq;
using pmp.sdk;
using pmp.sdk.model;

namespace TestConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = "https://api-sandbox.pmp.io/";
            
            // loading private credentials from plain text files in bin directory. API not public yet
            var clientId = File.ReadAllText("ClientId.txt");
            var clientSecret = File.ReadAllText("ClientSecret.txt");

            var api = new CollectionDoc(uri, new AuthClient(uri, clientId, clientSecret));
            var doc = api.GetDocument();

            var links = doc.GetQueryRelTypes().ToList();

            Console.WriteLine("-- Get all Rel types of Home document --");
            links.ForEach(Console.WriteLine);

            var obj = doc.Query("urn:collectiondoc:query:docs");

            Console.ReadLine();
        }
    }
}
