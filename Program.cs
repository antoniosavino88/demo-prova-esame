using System;
using System.Collections.Generic;
using System.Linq;

/*
 * TEMPLATE ESAME C# - NEGOZIO ONLINE
 *
 * Regola scelta per il template:
 * - circa il 30% dei metodi è già implementato, soprattutto dove c'è logica delicata
 *   come validazione quantità, aggiornamento magazzino, calcolo dei totali e storico acquisti.
 * - circa il 70% dei metodi contiene TODO guidati: lo studente deve completarli senza
 *   modificare firma, nome, parametri o tipo di ritorno.
 *
 * Vincolo richiesto: tutto il codice è in un unico file .cs e senza namespace.
 */

public class Program
{
    public static void Main()
    {
        // Punto di ingresso della Console App.
        ApplicazioneNegozio applicazione = new ApplicazioneNegozio();
        // applicazione.Avvia();
        TestNegozioOnline.EseguiTuttiITest();
    }
}

public class ApplicazioneNegozio
{
    private readonly CatalogoProdotti catalogoProdotti;
    private readonly CarrelloUtente carrelloUtente;
    private readonly StoricoAcquisti storicoAcquisti;
    private readonly ServizioNegozio servizioNegozio;

    public ApplicazioneNegozio()
    {
        catalogoProdotti = new CatalogoProdotti();
        carrelloUtente = new CarrelloUtente();
        storicoAcquisti = new StoricoAcquisti();
        servizioNegozio = new ServizioNegozio(catalogoProdotti, carrelloUtente, storicoAcquisti);

        CaricaDatiIniziali();
    }

    public void Avvia()
    {
        // TODO: implementare il ciclo principale della Console App.
        // Suggerimento:
        // 1. mostrare un messaggio di benvenuto;
        // 2. chiedere se l'utente vuole entrare come "utente" o "amministratore";
        // 3. chiamare GestisciMenuUtente oppure GestisciMenuAmministratore;
        // 4. permettere l'uscita dal programma con una scelta dedicata.
        throw new NotImplementedException("Completare il metodo Avvia.");
    }

    private void CaricaDatiIniziali()
    {
        // Metodo già implementato: fornisce prodotti di partenza per testare subito il sistema.
        catalogoProdotti.AggiungiProdotto(new Prodotto("P001", "Tastiera meccanica", 79.90m, 10));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P002", "Mouse wireless", 24.50m, 25));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P003", "Monitor 24 pollici", 149.99m, 7));
        catalogoProdotti.AggiungiProdotto(new Prodotto("P004", "Cavo USB-C", 9.99m, 40));
    }

    private string ScegliRuolo()
    {
        // TODO: leggere da console il ruolo scelto.
        // Valori consigliati: "utente", "amministratore", "esci".
        // Gestire input vuoti e maiuscole/minuscole con Trim() e ToLower().
        throw new NotImplementedException("Completare il metodo ScegliRuolo.");
    }

    private void GestisciMenuUtente()
    {
        // TODO: implementare il menu utente.
        // Operazioni richieste dalla traccia:
        // - visualizzare catalogo;
        // - aggiungere prodotto al carrello;
        // - visualizzare carrello;
        // - modificare quantità nel carrello;
        // - rimuovere prodotto dal carrello;
        // - svuotare carrello;
        // - confermare acquisto;
        // - visualizzare storico acquisti dell'utente.
        throw new NotImplementedException("Completare il metodo GestisciMenuUtente.");
    }

    private void GestisciMenuAmministratore()
    {
        // TODO: implementare il menu amministratore.
        // Operazioni richieste dalla traccia:
        // - visualizzare catalogo completo;
        // - aggiungere prodotto;
        // - eliminare prodotto;
        // - modificare prezzo;
        // - aumentare o diminuire quantità disponibile;
        // - visualizzare tutti gli acquisti;
        // - visualizzare quantità iniziale, venduta e disponibile per prodotto.
        throw new NotImplementedException("Completare il metodo GestisciMenuAmministratore.");
    }

    private void MostraCatalogo()
    {
        List<Prodotto> prodotti = catalogoProdotti.OttieniTuttiIProdotti();

        if (prodotti.Count == 0)
        {
            Console.WriteLine("Il catalogo è vuoto.");
            return;
        }

        Console.WriteLine("=== CATALOGO PRODOTTI ===");
        foreach (Prodotto p in prodotti)
        {
            Console.WriteLine($"[{p.CodiceProdotto}] {p.Nome} - €{p.Prezzo:F2} - Disponibili: {p.QuantitaDisponibile}");
        }
    }

    private void MostraCarrello()
    {
        List<ElementoCarrello> elementi = carrelloUtente.OttieniElementi();

        if (elementi.Count == 0)
        {
            Console.WriteLine("Il carrello è vuoto.");
            return;
        }

        Console.WriteLine("=== CARRELLO ===");
        foreach (ElementoCarrello e in elementi)
        {
            Console.WriteLine($"[{e.ProdottoSelezionato.CodiceProdotto}] {e.ProdottoSelezionato.Nome} - Qtà: {e.QuantitaScelta} x €{e.PrezzoUnitario:F2} = €{e.CalcolaTotaleParziale():F2}");
        }

        Console.WriteLine($"TOTALE: €{carrelloUtente.CalcolaTotale():F2}");
    }

    private void MostraStoricoUtente()
    {
        Console.Write("Inserire il nome utente: ");
        string nomeUtente = Console.ReadLine() ?? string.Empty;

        List<Acquisto> acquisti = storicoAcquisti.OttieniAcquistiPerUtente(nomeUtente);

        if (acquisti.Count == 0)
        {
            Console.WriteLine($"Nessun acquisto trovato per l'utente '{nomeUtente}'.");
            return;
        }

        Console.WriteLine($"=== STORICO ACQUISTI DI {nomeUtente} ===");
        foreach (Acquisto a in acquisti)
        {
            servizioNegozio.StampaAcquisto(a);
        }
    }

    private int LeggiInteroPositivo(string messaggio)
    {
        while (true)
        {
            Console.Write(messaggio);
            string input = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(input, out int valore) && valore > 0)
            {
                return valore;
            }

            Console.WriteLine("Valore non valido. Inserire un numero intero maggiore di zero.");
        }
    }

    private decimal LeggiPrezzoPositivo(string messaggio)
    {
        while (true)
        {
            Console.Write(messaggio);
            string input = Console.ReadLine() ?? string.Empty;

            if (decimal.TryParse(input, out decimal valore) && valore > 0)
            {
                return valore;
            }

            Console.WriteLine("Valore non valido. Inserire un prezzo maggiore di zero.");
        }
    }
}

public interface IGestioneCatalogo
{
    void AggiungiProdotto(Prodotto prodotto);
    bool EliminaProdotto(string codiceProdotto);
    Prodotto? CercaProdottoPerCodice(string codiceProdotto);
    List<Prodotto> OttieniTuttiIProdotti();
    bool ModificaPrezzoProdotto(string codiceProdotto, decimal nuovoPrezzo);
    bool ModificaQuantitaProdotto(string codiceProdotto, int variazioneQuantita);
}

public interface IGestioneCarrello
{
    bool AggiungiAlCarrello(Prodotto prodotto, int quantita);
    bool ModificaQuantitaNelCarrello(string codiceProdotto, int nuovaQuantita);
    bool RimuoviDalCarrello(string codiceProdotto);
    void SvuotaCarrello();
    decimal CalcolaTotale();
    List<ElementoCarrello> OttieniElementi();
}

public interface IGestioneAcquisti
{
    void RegistraAcquisto(Acquisto acquisto);
    List<Acquisto> OttieniTuttiGliAcquisti();
    List<Acquisto> OttieniAcquistiPerUtente(string nomeUtente);
}

public class Prodotto
{
    public string CodiceProdotto { get; private set; }
    public string Nome { get; private set; }
    public decimal Prezzo { get; private set; }
    public int QuantitaDisponibile { get; private set; }
    public int QuantitaIniziale { get; private set; }

    public Prodotto(string codiceProdotto, string nome, decimal prezzo, int quantitaDisponibile)
    {
        CodiceProdotto = codiceProdotto;
        Nome = nome;
        Prezzo = prezzo;
        QuantitaDisponibile = quantitaDisponibile;
        QuantitaIniziale = quantitaDisponibile;
    }

    public void CambiaPrezzo(decimal nuovoPrezzo)
    {
        // Metodo già implementato: centralizza la validazione del prezzo.
        if (nuovoPrezzo <= 0)
        {
            throw new ArgumentException("Il prezzo deve essere maggiore di zero.");
        }

        Prezzo = nuovoPrezzo;
    }

    public void CambiaQuantita(int variazioneQuantita)
    {
        // Metodo già implementato: impedisce di portare il magazzino sotto zero.
        int nuovaQuantita = QuantitaDisponibile + variazioneQuantita;

        if (nuovaQuantita < 0)
        {
            throw new InvalidOperationException("La quantità disponibile non può diventare negativa.");
        }

        QuantitaDisponibile = nuovaQuantita;
    }

    public int CalcolaQuantitaVenduta()
    {
        // Metodo già implementato: serve per il report amministratore.
        return QuantitaIniziale - QuantitaDisponibile;
    }
}

public class ElementoCarrello
{
    public Prodotto ProdottoSelezionato { get; private set; }
    public int QuantitaScelta { get; private set; }
    public decimal PrezzoUnitario { get; private set; }

    public ElementoCarrello(Prodotto prodottoSelezionato, int quantitaScelta)
    {
        ProdottoSelezionato = prodottoSelezionato;
        QuantitaScelta = quantitaScelta;
        PrezzoUnitario = prodottoSelezionato.Prezzo;
    }

    public decimal CalcolaTotaleParziale()
    {
        // Metodo già implementato: evita di duplicare il calcolo del parziale.
        return PrezzoUnitario * QuantitaScelta;
    }

    public void CambiaQuantitaScelta(int nuovaQuantita)
    {
        if (nuovaQuantita <= 0)
        {
            throw new ArgumentException("La quantità scelta deve essere maggiore di zero.");
        }

        QuantitaScelta = nuovaQuantita;
    }
}

public class Acquisto
{
    public string NomeUtente { get; private set; }
    public List<ElementoAcquistato> ProdottiAcquistati { get; private set; }
    public decimal TotaleOrdine { get; private set; }
    public DateTime DataAcquisto { get; private set; }

    public Acquisto(string nomeUtente, List<ElementoAcquistato> prodottiAcquistati)
    {
        NomeUtente = nomeUtente;
        ProdottiAcquistati = prodottiAcquistati;
        DataAcquisto = DateTime.Now;
        TotaleOrdine = CalcolaTotaleOrdine();
    }

    private decimal CalcolaTotaleOrdine()
    {
        // Metodo già implementato: somma tutti i parziali dei prodotti acquistati.
        return ProdottiAcquistati.Sum(prodotto => prodotto.TotaleParziale);
    }
}

public class ElementoAcquistato
{
    public string CodiceProdotto { get; private set; }
    public string NomeProdotto { get; private set; }
    public int QuantitaAcquistata { get; private set; }
    public decimal PrezzoUnitario { get; private set; }
    public decimal TotaleParziale { get; private set; }

    public ElementoAcquistato(string codiceProdotto, string nomeProdotto, int quantitaAcquistata, decimal prezzoUnitario)
    {
        CodiceProdotto = codiceProdotto;
        NomeProdotto = nomeProdotto;
        QuantitaAcquistata = quantitaAcquistata;
        PrezzoUnitario = prezzoUnitario;
        TotaleParziale = prezzoUnitario * quantitaAcquistata;
    }
}

public class CatalogoProdotti : IGestioneCatalogo
{
    private readonly List<Prodotto> prodotti;

    public CatalogoProdotti()
    {
        prodotti = new List<Prodotto>();
    }

    public void AggiungiProdotto(Prodotto prodotto)
    {
        // Metodo già implementato: evita codici duplicati nel catalogo.
        bool codiceGiaPresente = prodotti.Any(p => p.CodiceProdotto == prodotto.CodiceProdotto);

        if (codiceGiaPresente)
        {
            throw new InvalidOperationException("Esiste già un prodotto con lo stesso codice.");
        }

        prodotti.Add(prodotto);
    }

    public bool EliminaProdotto(string codiceProdotto)
    {
        Prodotto? prodotto = CercaProdottoPerCodice(codiceProdotto);

        if (prodotto == null)
        {
            return false;
        }

        prodotti.Remove(prodotto);
        return true;
    }

    public Prodotto? CercaProdottoPerCodice(string codiceProdotto)
    {
        // Metodo già implementato: ricerca case-insensitive per rendere più comodo l'input da console.
        return prodotti.FirstOrDefault(prodotto =>
            prodotto.CodiceProdotto.Equals(codiceProdotto, StringComparison.OrdinalIgnoreCase));
    }

    public List<Prodotto> OttieniTuttiIProdotti()
    {
        // Metodo già implementato: restituisce una copia per proteggere la lista interna.
        return new List<Prodotto>(prodotti);
    }

    public bool ModificaPrezzoProdotto(string codiceProdotto, decimal nuovoPrezzo)
    {
        Prodotto? prodotto = CercaProdottoPerCodice(codiceProdotto);

        if (prodotto == null)
        {
            return false;
        }

        prodotto.CambiaPrezzo(nuovoPrezzo);
        return true;
    }

    public bool ModificaQuantitaProdotto(string codiceProdotto, int variazioneQuantita)
    {
        Prodotto? prodotto = CercaProdottoPerCodice(codiceProdotto);

        if (prodotto == null)
        {
            return false;
        }

        prodotto.CambiaQuantita(variazioneQuantita);
        return true;
    }
}

public class CarrelloUtente : IGestioneCarrello
{
    private readonly List<ElementoCarrello> elementiCarrello;

    public CarrelloUtente()
    {
        elementiCarrello = new List<ElementoCarrello>();
    }

    public bool AggiungiAlCarrello(Prodotto prodotto, int quantita)
    {
        if (quantita <= 0)
        {
            return false;
        }

        if (quantita > prodotto.QuantitaDisponibile)
        {
            return false;
        }

        ElementoCarrello? elementoEsistente = elementiCarrello
            .FirstOrDefault(e => e.ProdottoSelezionato.CodiceProdotto == prodotto.CodiceProdotto);

        if (elementoEsistente != null)
        {
            int nuovaQuantitaTotale = elementoEsistente.QuantitaScelta + quantita;

            if (nuovaQuantitaTotale > prodotto.QuantitaDisponibile)
            {
                return false;
            }

            elementoEsistente.CambiaQuantitaScelta(nuovaQuantitaTotale);
        }
        else
        {
            elementiCarrello.Add(new ElementoCarrello(prodotto, quantita));
        }

        return true;
    }

    public bool ModificaQuantitaNelCarrello(string codiceProdotto, int nuovaQuantita)
    {
        ElementoCarrello? elemento = elementiCarrello
            .FirstOrDefault(e => e.ProdottoSelezionato.CodiceProdotto == codiceProdotto);

        if (elemento == null)
        {
            return false;
        }

        if (nuovaQuantita <= 0)
        {
            return false;
        }

        if (nuovaQuantita > elemento.ProdottoSelezionato.QuantitaDisponibile)
        {
            return false;
        }

        elemento.CambiaQuantitaScelta(nuovaQuantita);
        return true;
    }

    public bool RimuoviDalCarrello(string codiceProdotto)
    {
        ElementoCarrello? elemento = elementiCarrello
            .FirstOrDefault(e => e.ProdottoSelezionato.CodiceProdotto == codiceProdotto);

        if (elemento == null)
        {
            return false;
        }

        elementiCarrello.Remove(elemento);
        return true;
    }

    public void SvuotaCarrello()
    {
        // Metodo già implementato: cancella tutti gli elementi del carrello.
        elementiCarrello.Clear();
    }

    public decimal CalcolaTotale()
    {
        // Metodo già implementato: ricalcola sempre il totale dai parziali correnti.
        return elementiCarrello.Sum(elemento => elemento.CalcolaTotaleParziale());
    }

    public List<ElementoCarrello> OttieniElementi()
    {
        // Metodo già implementato: restituisce una copia per evitare modifiche esterne dirette.
        return new List<ElementoCarrello>(elementiCarrello);
    }
}

public class StoricoAcquisti : IGestioneAcquisti
{
    private readonly List<Acquisto> acquisti;

    public StoricoAcquisti()
    {
        acquisti = new List<Acquisto>();
    }

    public void RegistraAcquisto(Acquisto acquisto)
    {
        // Metodo già implementato: conserva l'acquisto in memoria durante l'esecuzione.
        acquisti.Add(acquisto);
    }

    public List<Acquisto> OttieniTuttiGliAcquisti()
    {
        // Metodo già implementato: restituisce una copia dello storico.
        return new List<Acquisto>(acquisti);
    }

    public List<Acquisto> OttieniAcquistiPerUtente(string nomeUtente)
    {
        return acquisti
            .Where(a => a.NomeUtente.Equals(nomeUtente, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}

public class ServizioNegozio
{
    private readonly CatalogoProdotti catalogoProdotti;
    private readonly CarrelloUtente carrelloUtente;
    private readonly StoricoAcquisti storicoAcquisti;

    public ServizioNegozio(CatalogoProdotti catalogoProdotti, CarrelloUtente carrelloUtente, StoricoAcquisti storicoAcquisti)
    {
        this.catalogoProdotti = catalogoProdotti;
        this.carrelloUtente = carrelloUtente;
        this.storicoAcquisti = storicoAcquisti;
    }

    public bool AggiungiProdottoAlCarrello(string codiceProdotto, int quantita)
    {
        Prodotto? prodotto = catalogoProdotti.CercaProdottoPerCodice(codiceProdotto);

        if (prodotto == null)
        {
            return false;
        }

        return carrelloUtente.AggiungiAlCarrello(prodotto, quantita);
    }

    public Acquisto ConfermaAcquisto(string nomeUtente)
    {
        // Metodo già implementato: è una delle logiche più importanti della traccia.
        // 1. impedisce acquisti con carrello vuoto;
        // 2. ricontrolla la disponibilità prima di scalare il magazzino;
        // 3. crea una copia dei dati acquistati;
        // 4. aggiorna il magazzino;
        // 5. registra l'acquisto nello storico;
        // 6. svuota il carrello.
        List<ElementoCarrello> elementi = carrelloUtente.OttieniElementi();

        if (elementi.Count == 0)
        {
            throw new InvalidOperationException("Non è possibile confermare un acquisto con carrello vuoto.");
        }

        foreach (ElementoCarrello elemento in elementi)
        {
            if (elemento.QuantitaScelta <= 0)
            {
                throw new InvalidOperationException("Nel carrello è presente una quantità non valida.");
            }

            if (elemento.QuantitaScelta > elemento.ProdottoSelezionato.QuantitaDisponibile)
            {
                throw new InvalidOperationException("La quantità richiesta supera la disponibilità di magazzino.");
            }
        }

        List<ElementoAcquistato> prodottiAcquistati = elementi
            .Select(elemento => new ElementoAcquistato(
                elemento.ProdottoSelezionato.CodiceProdotto,
                elemento.ProdottoSelezionato.Nome,
                elemento.QuantitaScelta,
                elemento.PrezzoUnitario))
            .ToList();

        foreach (ElementoCarrello elemento in elementi)
        {
            elemento.ProdottoSelezionato.CambiaQuantita(-elemento.QuantitaScelta);
        }

        Acquisto acquisto = new Acquisto(nomeUtente, prodottiAcquistati);
        storicoAcquisti.RegistraAcquisto(acquisto);
        carrelloUtente.SvuotaCarrello();

        return acquisto;
    }

    public List<ReportProdotto> CreaReportProdotti()
    {
        // Metodo già implementato: prepara il report richiesto per l'amministratore.
        return catalogoProdotti.OttieniTuttiIProdotti()
            .Select(prodotto => new ReportProdotto(
                prodotto.CodiceProdotto,
                prodotto.Nome,
                prodotto.QuantitaIniziale,
                prodotto.CalcolaQuantitaVenduta(),
                prodotto.QuantitaDisponibile))
            .ToList();
    }

    public void StampaAcquisto(Acquisto acquisto)
    {
        Console.WriteLine($"Utente: {acquisto.NomeUtente} - Data: {acquisto.DataAcquisto:dd/MM/yyyy HH:mm}");

        foreach (ElementoAcquistato ea in acquisto.ProdottiAcquistati)
        {
            Console.WriteLine($"  [{ea.CodiceProdotto}] {ea.NomeProdotto} - Qtà: {ea.QuantitaAcquistata} x €{ea.PrezzoUnitario:F2} = €{ea.TotaleParziale:F2}");
        }

        Console.WriteLine($"  Totale ordine: €{acquisto.TotaleOrdine:F2}");
    }

    public void StampaReportProdotti()
    {
        List<ReportProdotto> report = CreaReportProdotti();

        if (report.Count == 0)
        {
            Console.WriteLine("Nessun prodotto nel catalogo.");
            return;
        }

        Console.WriteLine("=== REPORT PRODOTTI ===");
        foreach (ReportProdotto r in report)
        {
            Console.WriteLine($"[{r.CodiceProdotto}] {r.NomeProdotto} - Iniziale: {r.QuantitaIniziale} | Venduta: {r.QuantitaVenduta} | Disponibile: {r.QuantitaDisponibile}");
        }
    }
}

public class ReportProdotto
{
    public string CodiceProdotto { get; private set; }
    public string NomeProdotto { get; private set; }
    public int QuantitaIniziale { get; private set; }
    public int QuantitaVenduta { get; private set; }
    public int QuantitaDisponibile { get; private set; }

    public ReportProdotto(string codiceProdotto, string nomeProdotto, int quantitaIniziale, int quantitaVenduta, int quantitaDisponibile)
    {
        CodiceProdotto = codiceProdotto;
        NomeProdotto = nomeProdotto;
        QuantitaIniziale = quantitaIniziale;
        QuantitaVenduta = quantitaVenduta;
        QuantitaDisponibile = quantitaDisponibile;
    }
}
