using precios;
using System.Text.Json;
internal class Progama{
    private static void mostrar(Root raiz){
        Console.WriteLine("===MONEDAS===");
        Console.WriteLine("EUR: €"+raiz.bpi.EUR.rate_float);
        Console.WriteLine("USD: $"+raiz.bpi.USD.rate_float);
        Console.WriteLine("GBP: £"+raiz.bpi.GBP.rate_float);
    }
    public static async Task<Root?> Raiz(){
        var pedirURL="https://api.coindesk.com/v1/bpi/currentprice.json";
        using(HttpClient cliente=new HttpClient){
            cliente.DefaultRequestHeaders.Accept.Clear();
            cliente.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage respuesta = await cliente.GetAsync(pedirURL);
            if(respuesta.IsSuccessStatusCode){
                var respBody=await respuesta.Content.ReadAsStringAsync();
                var prueba=JsonSerializer.Deserialize<Root>(respBody);
                return prueba;
            }else{
                return null;
            }
        }
    }
    private static void main(){
        int aux1;
        Root? raiz=Raiz().Result;
        mostrar(raiz);
        Console.WriteLine("\nDe que moneda desea saber mas informacion?\n1=EUR\n2=USD\n3=GBP");
        string? aux2=Console.ReadLine();
        while(!int.TryParse(aux2, out aux1)) {
            Console.WriteLine("\nIngresar una opcion valida");
            aux2 = Console.ReadLine();
        }
        precios.Tipo tipo=(precios.Tipo)aux1;
        Console.WriteLine("===Informacion sobre la moneda seleccionada===");
        switch(tipo){
            case precios.Tipo.EUR:
                Console.WriteLine("Codigo: " + raiz.bpi.EUR.code);
                Console.WriteLine("Simbolo: " + raiz.bpi.EUR.symbol);
                Console.WriteLine("Frecuencia: " + raiz.bpi.EUR.rate);
                Console.WriteLine("Descripcion: " + raiz.bpi.EUR.description);
                Console.WriteLine("Precio: " + raiz.bpi.EUR.rate_float);
                break;
            case precios.Tipo.USD:
                Console.WriteLine("Codigo: " + raiz.bpi.USD.code);
                Console.WriteLine("Simbolo: " + raiz.bpi.USD.symbol);
                Console.WriteLine("Frecuencia: " + raiz.bpi.USD.rate);
                Console.WriteLine("Descripcion: " + raiz.bpi.USD.description);
                Console.WriteLine("Precio: " + raiz.bpi.USD.rate_float);
                break;
            case precios.Tipo.GBP:
                Console.WriteLine("Codigo: " + raiz.bpi.GBP.code);
                Console.WriteLine("Simbolo: " + raiz.bpi.GBP.symbol);
                Console.WriteLine("Frecuencia: " + raiz.bpi.GBP.rate);
                Console.WriteLine("Descripcion: " + raiz.bpi.GBP.description);
                Console.WriteLine("Precio: " + raiz.bpi.GBP.rate_float);
                break;
            default:
                break;
        }
    }
}