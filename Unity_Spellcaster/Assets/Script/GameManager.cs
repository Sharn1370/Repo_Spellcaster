using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    public Text deckSizeText;
    public TMP_Text typedTextDisplay;
    public TMP_Text spelledWordDisplay;
    private string currentTypedText = "";
    public static GameManager Instance { get; private set; }


    private void Start()
    {
        // Ensure all slots are marked available
        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            availableCardSlots[i] = true;
        }

        UpdateDeckSizeText(); // Initialize UI on start

        // Ensure deck has enough cards before drawing
        if (deck.Count >= 10)
        {
            StartCoroutine(DrawInitialCards(10, 0.5f)); // Draw 10 cards with a 0.5s delay
        }
        else
        {
            Debug.LogWarning("Not enough cards in the deck to draw 10!");
        }
    }

    private void Awake()
{
    Instance = this;
}

private void Update()
{
    HandleTypingInput(); // Add this to hook into Unity's frame update
    //handlecardinput?
}

 public void OnCardPressed(char letter)
    {
        // Process the letter just like it's typed from the keyboard
        string allowedChars = "AEIOTRSPLC";  // You can customize this to match your allowed letters

        // Make sure the letter is valid and within the allowed characters
        if (allowedChars.Contains(letter) && currentTypedText.Length < 10)
        {
            currentTypedText += letter;  // Add the letter to the typed text
            typedTextDisplay.color = new Color(typedTextDisplay.color.r, typedTextDisplay.color.g, typedTextDisplay.color.b, 1f);
            typedTextDisplay.text = currentTypedText;  // Update the display
        }
    }

private void HandleTypingInput()
{
    string allowedChars = "AEIOTRSPLC";

    foreach (char c in Input.inputString.ToUpper())
    {
        if (c == '\b') // Backspace
        {
            if (currentTypedText.Length > 0)
                currentTypedText = currentTypedText.Substring(0, currentTypedText.Length - 1);
        }
        else if (allowedChars.Contains(c) && currentTypedText.Length < 10)
        {
            currentTypedText += c;
        }

        if (typedTextDisplay != null)
{
    typedTextDisplay.color = new Color(typedTextDisplay.color.r, typedTextDisplay.color.g, typedTextDisplay.color.b, 1f);
    typedTextDisplay.text = currentTypedText;
}
    }
     ValidateTypedWord();
}

 private void ValidateTypedWord()
    {
//         string[] validWords = { 
//     "aisle", "ale", "alp", "alt", "ape", "arc", "art", "as", "at", "ear", "eat", "els", 
//     "ice", "isle", "it", "let", "lie", "lip", "lit", "op", "opt", "pail", "pale", "par", 
//     "pat", "pile", "pit", "plate", "plot", "pore", "port", "price", "rail", "races", "rat", 
//     "rip", "rise", "role", "salt", "sari", "sire", "slip", "sloe", "slot", "tale", "tap", 
//     "tip", "tile", "to", "top" 
// };  // List of valid words

string[] validWords = {
    "to", "so", "it", "is", "as", "ace", "act", "ail", "air", "ais", 
    "ait", "ale", "alp", "als", "alt", "ape", "apo", "apt", "arc", "are", "ars", "art", "asp", "ate", "cal", 
    "cap", "car", "cat", "cel", "cep", "cis", "col", "cop", "cor", "cos", "cot", "ear", "eat", "eco", "els", 
    "era", "ers", "est", "eta", "ice", "ire", "its", "lac", "lap", "lar", "las", "lat", "lea", "lei", "les", "let", 
    "lie", "lip", "lis", "lit", "lop", "lor", "lot", "oar", "oat", "oca", "oes", "oil", "ole", "opa", "ope", "ops", "opt", 
    "ora", "orc", "ore", "ors", "ort", "ose", "pac", "pal", "par", "pas", "pat", "pea", "pec", "per", "pes", "pet", "pia", "pic", 
    "pie", "pis", "pit", "poi", "pol", "pos", "pot", "pro", "psi", "pst", "rai", "rap", "ras", "rat", "rec", "rei", "rep", "res", "ret", "ria", 
    "rip", "roc", "roe", "rot", "sac", "sae", "sal", "sap", "sat", "sea", "sec", "sei", "sel", "ser", "set", "sic", "sip", "sir", "sit", "soc", "sol", 
    "sop", "sot", "spa", "sri", "tae", "tao", "tap", "tar", "tas", "tea", "tec", "tel", "tes", "tic", "tie", "til", "tip", "tis", "toe", "top", "tor",
    "aces", "acre", "acro", "acts", "aero", "ails", "airs", "airt", "aits", "alec", "ales", "alit", "aloe", "alps", "also", "alto", "alts", "aper", "apes", 
    "apos", "apse", "arco", "arcs", "ares", "aril", "arse", "arts", "asci", "ates", "atop", "calo", "cals", "cape", "capo", "caps", "care", "carl", "carp", 
    "cars", "cart", "case", "cast", "cate", "cats", "ceil", "cels", "celt", "ceps", "cero", "cert", "ciao", "cire", "cist", "cite", "clap", "clip", "clop", 
    "clot", "coal", "coat", "coil", "coir", "cola", "cole", "cols", "colt", "cope", "cops", "core", "cors", "cost", "cote", "cots", "crap", "crip", "cris", 
    "crit", "crop", "earl", "ears", "east", "eats", "ecos", "epic", "epos", "eras", "eros", "erst", "etas", "etic", "ices", "ilea", "iota", "ires", "isle", 
    "lace", "lacs", "laic", "lair", "laps", "lari", "lars", "lase", "last", "late", "lati", "lats", "leap", "lear", "leas", "leis", "lept", "lest", "lets", 
    "liar", "lias", "lice", "lier", "lies", "lipa", "lipe", "lipo", "lips", "lira", "lire", "lisp", "list", "lite", "lits", "loca", "loci", "lope", "lops", 
    "lore", "lose", "lost", "lota", "loti", "lots", "oars", "oast", "oats", "ocas", "oils", "olea", "oles", "opal", "opas", "opes", "opts", "oral", "orca", 
    "orcs", "ores", "orle", "orts", "osar", "otic", "pace", "pacs", "pact", "pail", "pair", "pale", "pali", "pals", "pare", "pars", "part", "pase", "past", 
    "pate", "pats", "peal", "pear", "peas", "peat", "pecs", "pelt", "perc", "peri", "pert", "peso", "pest", "pets", "pial", "pias", "pica", "pice", "pics", 
    "pier", "pies", "pile", "piso", "pita", "pits", "plat", "plea", "plie", "plot", "poet", "pois", "pole", "pols", "pore", "port", "pose", "post", "pots", 
    "prao", "prat", "proa", "pros", "race", "rail", "rais", "rale", "rape", "raps", "rapt", "rase", "rasp", "rate", "rato", "rats", "real", "reap", "recs", 
    "reis", "repo", "reps", "rest", "rets", "rial", "rias", "rice", "riel", "rile", "riot", "ripe", "rips", "rise", "rite", "rocs", "roes", "roil", "role", 
    "rope", "rose", "rota", "rote", "roti", "rotl", "rots", "sail", "sale", "salp", "salt", "sari", "sate", "sati", "scar", "scat", "scop", "scot", "seal", 
    "sear", "seat", "sect", "sept", "sera", "seta", "sial", "sice", "silo", "silt", "sipe", "sire", "site", "slap", "slat", "slip", "slit", "sloe", "slop", 
    "slot", "soap", "soar", "soca", "soil", "sola", "sole", "soli", "sora", "sore", "sori", "sort", "spae", "spar", "spat", "spec", "spit", "spot", "star", 
    "step", "stir", "stoa", "stop", "tace", "taco", "tael", "tail", "talc", "tale", "tali", "taos", "tape", "taps", "tare", "taro", "tarp", "tars", "tase", 
    "teal", "tear", "teas", "tecs", "tela", "tels", "tepa", "tics", "tier", "ties", "tile", "tils", "tips", "tire", "tirl", "tiro", "toea", "toes", "toil", 
    "tola", "tole", "tope", "topi", "tops", "tora", "torc", "tore", "tori", "tors", "tosa", "trap", "tres", "trio", "trip", "trop", "tsar", "acres", "acros", "actor",
    "airts", "aisle", "alecs", "alert", "alist", "aloes", "alter", "altos", "apers", "aport", "apres", "apter", "areic", "ariel", "arils", "arise", "arles", 
    "arose", "artel", "ascot", "asper", "aspic", "aster", "astir", "atrip", "calos", "caper", "capes", "capos", "capot", "capri", "cares", "caret", "carle", 
    "carls", "carol", "carpi", "carps", "carse", "carte", "carts", "caste", "cater", "cates", "ceils", "celts", "ceorl", "ceria", "ceros", "certs", "cesta", 
    "cesti", "cires", "citer", "cites", "claps", "clapt", "claro", "clasp", "clast", "clear", "cleat", "clept", "clips", "clipt", "clits", "clops", "close",
     "clots", "coals", "coapt", "coast", "coati", "coats", "coils", "coirs", "colas", "coles", "colts", "copal", "coper", "copes", "copra", "copse", "coral", 
     "cores", "coria", "corps", "corse", "coset", "cosie", "costa", "cotes", "crape", "craps", "crate", "crept", "crest", "cries", "cripe", "crips", "crisp", 
     "crits", "crops", "earls", "eclat", "epact", "epics", "erica", "escar", "escot", "estop", "etics", "ileac", "iotas", "irate", "islet", "istle", "lacer", 
     "laces", "laics", "lairs", "lapis", "lapse", "lares", "laris", "laser", "later", "leaps", "leapt", "lears", "least", "lepta", "liars", "liers", "lipas", "lipos",
      "liras", "lirot", "litas", "liter", "lites", "litre", "locie", "locis", "loper", "lopes", "lores", "loris", "loser", "lotas", "lotic", "lotsa", "oater", "ocrea",
       "octal", "oiler", "oleic", "opals", "opera", "optic", "orals", "orate", "orcas", "oriel", "orles", "oscar", "osier", 
       "ostia", "pacer", "paces", "pacts", "pails", "pairs", "paise", "paler", "pales", "palet", "palis", "pareo", "pares", "paris", "parle", "parol", "parse", 
       "parts", "paseo", "paste", "pater", "pates", "patio", "peals", "pearl", "pears", "peart", "peats", "pelts", "percs", "peril", "peris", "pesto", "petal", 
       "pical", "picas", "picot", "piers", "pieta", "pilar", "pilea", "piles", "pilot", "pisco", "piste", "pitas", "place", "plait", "plate", "plats", "pleas", 
       "pleat", "plica", "plier", "plies", "plots", "poets", "poise", "polar", "poler", "poles", "polis", "pores", "ports", "poser", "posit", "praos", "prase", 
       "prate", "prats", "presa", "prest", "price", "pries", "prise", "proas", "prole", "prose", "prost", "psoae", "psoai", "races", "rails", "raise", "rales", 
       "rapes", "ratel", "rates", "ratio", "ratos", "react", "reais", "reals", "reaps", "recap", "recit", "recta", "recti", "recto", "relic", "relit", "reoil", 
       "repos", "repot", "resat", "resit", "retia", "rials", "rices", "riels", "riles", "riots", "ripes", "rites", "roast", "roils", "roles", "ropes", "roset", 
       "rosti", "rotas", "rotes", "rotis", "rotls", "saice", "salep", "salic", "sapor", "scale", "scalp", "scape", "scare", "scarp", "scart", "scopa", "scope", 
       "score", "scrap", "scrip", "sepal", "sepia", "sepic", "septa", "serac", "serai", "seral", "setal", "sitar", "slate", "slept", "slice", "slier", "slipe", 
       "slipt", "slope", "socle", "solar", "solei", "sorel", "sorta", "space", "spail", "spait", "spale", "spare", "spate", "spear", "spect", "speil", "speir", 
       "spelt", "spica", "spice", "spiel", "spier", "spile", "spilt", "spire", "spirt", "spite", "splat", "split", "spoil", "spore", "sport", "sprat", "sprit", 
       "stair", "stale", "stare", "steal", "stela", "stile", "stipe", "stirp", "stoae", "stoai", "stoic", "stole", "stope", "store", "strap", "strep", "stria", 
       "strip", "strop", "taces", "tacos", "taels", "tails", "talcs", "taler", "tales", "taper", "tapes", "tapir", "tapis", "tares", "taroc", "taros", "tarps", 
       "tarsi", "taser", "teals", "tears", "telco", "telia", "telic", "teloi", "telos", "tepal", "tepas", "terai", "tesla", "tical", "tiers", "tiler", "tiles", 
       "tires", "tirls", "tiros", "toeas", "toile", "toils", "tolar", "tolas", "toles", "toper", "topes", "topic", "topis", "toras", "torcs", "tores", "toric", 
       "torse", "torsi", "trace", "trail", "traps", "triac", "trial", "trice", "tries", "triol", "trios", "tripe", "trips", "trois", "trope", "actors", "airest", 
       "alerts", "alters", "aorist", "aortic", "apices", "aplite", "ariels", "ariose", "aristo", "artels", "artsie", "aslope", "aspect", "aspire", "atelic", "atopic",
        "capers", "caplet", "capote", "capots", "capris", "captor", "carets", "caries", "carles", "caroli", "carols", "carpel", "carpet", "cartel", "cartes", "cartop", 
        "caster", "castle", "castor", "caters", "ceorls", "cerias", "cestoi", "citers", "citola", "citole", "citral", "claret", "claros", "claspt", "clears", "cleats", 
        "closer", "closet", "coaler", "coapts", "coarse", "coater", "coatis", "coiler", "coital", "colies", "colter", "copals", "copers", "copier", "copies", "copras", 
        "copter", "corals", "corpse", "corset", "cosier", "costae", "costal", "costar", "coster", "crapes", "crates", "cresol", "cripes", "crista", "eclair", "eclats",
         "epacts", "epical", "ericas", "erotic", "escarp", "escort", "espial", "espoir", "esprit", "estral", "lacers", "lacier", "lapser", "laster", "lector", "lictor",
          "lipase", "lisper", "lister", "liters", "litres", "locate", "locies", "loiter", "lopers", "lorica", "lories", "oaters", "oatier", "ocreas", "oilers", "operas",
           "opiate", "optics", "oracle", "orates", "oriels", "osetra", "osteal", "ostler", "pacers", "pacier", "palest", "palets", "palier", "palter", "parcel", "pareos", 
           "paries", "parles", "parole", "parols", "parsec", "pastel", "paster", "pastie", "pastil", "pastor", "paters", "patios", "patois", "patrol", "pearls", "pelota",
            "perils", "petals", "petrol", "petsai", "picaro", "picots", "pietas", "pilose", "pilots", "piolet", "pirate", "pistol", "placer", "places", "placet", "plaice",
             "plaits", "plater", "plates", "pleats", "plicae", "plicas", "pliers", "poetic", "poiser", "polars", "poleis", "polers", "police", "polies", "polite", "portal",
              "posier", "postal", "poster", "postie", "potsie", "praise", "prates", "pratie", "preact", "precis", "presto", "prices", "priest", "proles", "prosit", "protea",
               "protei", "racist", "rapist", "ratels", "ratios", "reacts", "recaps", "recast", "recits", "recoal", "recoat", "recoil", "rectal", "rectos", "relics", "relict",
                "relist", "reoils", "repast", "replot", "repots", "resail", "respot", "retail", "retial", "rialto", "rictal", "ripest", "ripost", "rosace", "sailer", "sailor",
                 "salter", "saltie", "sapote", "satire", "satori", "scaler", "sclera", "scopae", "scoria", "scoter", "scotia", "scrape", "script", "scrota", "secpar", "sector",
                  "septal", "septic", "serail", "serial", "slater", "solace", "solepi", "solita", "sortie", "spacer", "splice", "spoilt", "spolet", "sprace", "stable", "staple", 
                  "stelai", "stical", "stoaei", "stoic", "strap", "taiper", "talers", "talipes", "teapoi", "tercia", "terpic", "tipers", "tiscal", "toecap", "toiler", "topics", 
                  "topper", "torase", "torsal", "torsel", "tosier", "trails", "tripos", "troles", "tropes", "airpost", "aloetic", "aplites", "apostil", "apostle", "apricot", "aprotic", 
                  "article", "aseptic", "atopies", "atresic", "caliper", "calorie", "caltrop", "capitol", "caplets", "capotes", "captors", "cariole", "carpels", "carpets", 
                  "cartels", "celosia", "citolas", "citoles", "citrals", "clarets", "claries", "claroes", "clasper", "coalers", "coalier", "coalpit", "coaster", "coaters", 
                  "coilers", "colters", "copiers", "copters", "corslet", "costrel", "crestal", "cristae", "crotale", "eclairs", "ectopia", "elastic", "erotica", "erotics", 
                  "escalop", "escolar", "esparto", "estriol", "isolate", "laciest", "lactose", "latices", "lectors", "lictors", "locater", "locates", "loiters", "loricae",
                   "loricas", "olestra", "opiates", "optical", "oracies", "oracles", "oralist", "paciest", "paliest", "palsier", "palters", "parcels", "paretic", "paroles", 
                   "parotic", "parties", "pastier", "patrols", "peloria", "peloric", "pelotas", "persalt", "petrols", "piaster", "piastre", "picaros", "picrate", "piolets", 
                   "pirates", "pistole", "placers", "placets", "plaices", "plaiter", "plaster", "plastic", "platers", "platier", "platies", "plectra", "plicate", "poetics", 
                   "poitrel", "polecat", "policer", "polices", "politer", "portals", "praties", "preacts", "precast", "prolate", "prosaic", "prosect", "prostie", "proteas",
                    "psalter", "psoatic", "raciest", "realist", "recital", "reclasp", "recoals", "recoats", "recoils", "relicts", "replica", "replots", "reposal", "reposit", 
                    "resplit", "retails", "rialtos", "riposte", "ropiest", "saltier", "saltire", "scalier", "scalper", "scarlet", "scoriae", "scrapie", "scrotal", "seaport", 
                    "slatier", "soapier", "solacer", "spacier", "special", "spectra", "spicate", "splicer", "spoiler", "stapler", "stearic", "stoical", "tailers", "tailors", 
                    "talcier", "talcose", "talipes", "toecaps", "toeclip", "toilers", "topical", "topsail", "traipse", "triceps", "triples", "tropics", "apricots", "articles", 
                    "calipers", "calories", "caltrops", "capitols", "capriole", "carioles", "carspiel", "cloister", "coaliest", "coalpits", "coistrel", "costlier", "crispate", 
                    "crotales", "ectopias", "epilator", "erotical", "eroticas", "leprotic", "locaters", "loricate", "operatic", "parclose", "paretics", "particle", "pectoral", 
                    "pelorias", "petiolar", "petrolic", "petrosal", "picrates", "pilaster", "piscator", "plaister", "plaiters", "poetical", "poitrels", "polarise", "polecats", 
                    "polestar", "policers", "postrace", "practise", "prelatic", "recitals", "replicas", "sceptral", "sectoral", "septical", "septoria", "societal", "spectral", 
                    "spiracle", "spoliate", "sterical", "tieclasp", "toeclips", "topicals", "tropical", "caprioles", "epilators", "loricates", "operatics", "particles", "pectorals", 
                    "precoital", "saprolite", "scapolite", "sclerotia", "sectorial", "tropicals"};

        // Check if the current typed text matches any valid word
        foreach (string word in validWords)
        {
            if (currentTypedText.ToLower() == word.ToLower())  // Match found
            {
                // Display the word on the SpelledWordDisplay
                if (spelledWordDisplay != null)
                {
                    spelledWordDisplay.text = word;  // Update SpelledWordDisplay with the valid word
                }

                 StartCoroutine(FadeOutTypedText());
                 // Reset the current typed text
                //currentTypedText = "";

                // I want there to be a .5 second delay, and for the last word to fade out
                //typedTextDisplay.text = "";

                break; // Exit the loop once a valid word is found
            }
        }
    }


    public void DrawCard()
    {
        if (deck.Count > 0)
        {
            Card randCard = deck[Random.Range(0, deck.Count)];

            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i]) // Ensure slot is available
                {
                    randCard.gameObject.SetActive(true);
                    randCard.transform.position = cardSlots[i].position;
                    availableCardSlots[i] = false;
                    deck.Remove(randCard);

                    // Prevent UI blocking
                    randCard.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

                    UpdateDeckSizeText();
                    return;
                }
            }
        }
        else
        {
            Debug.LogWarning("Deck is empty! No cards to draw.");
        }
    }

    private IEnumerator DrawInitialCards(int numberOfCards, float delay)
    {
        yield return new WaitForSeconds(0.1f); // Prevents first card being skipped

        for (int i = 0; i < numberOfCards; i++)
        {
            DrawCard();
            yield return new WaitForSeconds(delay);
        }
    }

    private void UpdateDeckSizeText()
    {
        if (deckSizeText != null)
        {
            deckSizeText.text = deck.Count.ToString();
        }
    }

private IEnumerator FadeOutTypedText()
{
    Color originalColor = typedTextDisplay.color;
    Color fadedColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

    float fadeDuration = 0.5f;
    float elapsedTime = 0f;

    while (elapsedTime < fadeDuration)
    {
        typedTextDisplay.color = Color.Lerp(originalColor, fadedColor, elapsedTime / fadeDuration);
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    typedTextDisplay.color = fadedColor;

    yield return new WaitForSeconds(0.5f);

    // Reset everything here AFTER fade-out finishes
    currentTypedText = "";
    typedTextDisplay.text = "";
    typedTextDisplay.color = originalColor; // Restore color for next input
}

    
}