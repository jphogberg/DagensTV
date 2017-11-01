/* Autocomplete */
var tvListings = [
    /* SVT1 */
    {
        label: "Morgonstudion",
        time: "06:00",
        channel: "SVT1"
    },
    {
        label: "Go'kväll",
        time: "09:10",
        time2: "18:45",        
        channel: "SVT1"
    },
    {
        label: "Natur så in i Norden",
        time: "09:55",
        channel: "SVT1"
    },
    {
        label: "Mitt i Naturen",
        time: "10:25",
        channel: "SVT1"
    },
    {
        label: "Heja Kosovo friskt humör",
        time: "11:25",
        time2: "23:55",
        channel: "SVT1"
    },        
    {
        label: "Bästa mannen",
        time: "12:25",
        channel: "SVT1"
    },
    {
        label: "Opinion live",
        time: "13:25",
        channel: "SVT1"
    },
    {
        label: "Vår tid är nu",
        time: "14:10",
        channel: "SVT1"
    },
    {
        label: "Engelska Antikrundan",
        time: "15:10",
        time2: "18:00",
        channel: "SVT1",
        channel2: "SVT2"
    },
    {
        label: "Karl för sin kilt",
        time: "16:10",
        channel: "SVT1"
    },
    {
        label: "Vem vet mest?",
        time: "17:00",
        time2: "19:00",
        channel: "SVT1",
        channel2: "SVT2"
    },
    {
        label: "Sverige idag",
        time: "17:30",
        channel: "SVT1"
    },
    {
        label: "Rapport",
        time: "18:00",
        time2: "19:30",
        time3: "23:05",
        channel: "SVT1"
    },
    {
        label: "Kulturnyheterna",
        time: "18:13",
        channel: "SVT1"
    },
    {
        label: "Sportnytt",
        time: "18:25",
        time2: "21:30",
        channel: "SVT1",
        channel2: "SVT2"
    },
    {
        label: "Lokala nyheter",
        time: "18:30",
        time2: "19:55",
        channel: "SVT1"
    },
    {
        label: "Doobidoo",
        time: "20:00",
        channel: "SVT1"
    },
    {
        label: "Skavlan",
        time: "21:00",
        channel: "SVT1"
    },
    {
        label: "Stan Lee's Lucky Man",
        time: "22:05",
        channel: "SVT1"
    },
    {
        label: "Släng dig i brunnen",
        time: "22:50",
        channel: "SVT1"
    },
    {
        label: "American Odyssey",
        time: "23:10",
        channel: "SVT1"
    },
    {
        label: "30 September",
        time: "00:55",
        channel: "SVT1"
    },
    /* SVT2 */
    {
        label: "Förväxlingen",
        time: "08:15",
        time2: "19:30",
        channel: "SVT2"
    },
    {
        label: "Forum: Moderaternas partistämma",
        time: "08:45",
        channel: "SVT2"
    },
    {
        label: "Nyheter på lätt svenska",
        time: "17:15",
        channel: "SVT2"
    },
    {
        label: "Nyhetstecken",
        time: "17:20",
        channel: "SVT2"
    },
    {
        label: "Oddasat",
        time: "17:30",
        channel: "SVT2"
    },
    {
        label: "Uutiset",
        time: "17:45",
        channel: "SVT2"
    },
    {
        label: "Flamencodrottningen La Chana",
        time: "20:00",
        channel: "SVT2"
    },
    {
        label: "Aktuellt",
        time: "21:00",
        channel: "SVT2"
    },
    {
        label: "Jätten",
        time: "21:45",
        channel: "SVT2"
    },
    {
        label: "Världens bästa veterinär - special",
        time: "23:15",
        channel: "SVT2"
    },
    {
        label: "Hundra procent bonde",
        time: "00:05",
        channel: "SVT2"
    },
    {
        label: "Nyheter",
        time: "01:00",
        channel: "SVT2"
    },
    /* TV3 */
    {
        label: "The Nanny",
        time: "06:00",
        channel: "TV3"
    },
    {
        label: "Neighbours",
        time: "06:30",
        channel: "TV3"
    },
    {
        label: "Aftonbladet morgon",
        time: "07:00",
        channel: "TV3"
    },
    {
        label: "The Taste US",
        time: "09:00",
        channel: "TV3"
    },
    {
        label: "Navy CIS",
        time: "09:55",
        channel: "TV3"
    },
    {
        label: "The Mentalist",
        time: "10:55",
        channel: "TV3"
    },
    {
        label: "Lyxfällan",
        time: "11:55",
        time2: "15:55",
        channel: "TV3"
    },
    {
        label: "Project Runway",
        time: "12:55",
        time2: "17:00",
        channel: "TV3"
    },
    {
        label: "The Real Housewives of Atlanta",
        time: "13:55",
        time2: "18:00",
        channel: "TV3"
    },
    {
        label: "The Lie Detective",
        time: "14:55",
        channel: "TV3"
    },
    {
        label: "Unga mammor",
        time: "19:00",
        time2: "19:30",
        channel: "TV3"
    },
    {
        label: "Safe Haven",
        time: "20:00",
        channel: "TV3"
    },
    {
        label: "Fighting",
        time: "22:25",
        channel: "TV3"
    },
    {
        label: "Think Like a Man",
        time: "00:35",
        channel: "TV3"
    },
    {
        label: "Chicago PD",
        time: "02:50",
        channel: "TV3"
    },
    {
        label: "Paradise Hotel",
        time: "03:35",
        channel: "TV3"
    },
    /* TV4 */
    {
        label: "Malou efter tio",
        time: "10:00",
        channel: "TV4"
    },
    {
        label: "Hem till gården",
        time: "12:00",
        channel: "TV4"
    },
    {
        label: "Hela England bakar junior",
        time: "12:30",
        channel: "TV4"
    },
    {
        label: "Mord och inga visor",
        time: "13:00",
        time2: "13:55",
        channel: "TV4"
    },
    {
        label: "Antiques Roadshow",
        time: "14:55",
        channel: "TV4"
    },
    {
        label: "En plats i solen: Sommarsol",
        time: "15:55",
        channel: "TV4"
    },
    {
        label: "Fixer Upper",
        time: "16:50",
        channel: "TV4"
    },
    {
        label: "V75-Klubben",
        time: "17:50",
        channel: "TV4"
    },
    {
        label: "112 - på liv och död",
        time: "17:55",
        channel: "TV4"
    },
    {
        label: "Keno",
        time: "18:50",
        channel: "TV4"
    },
    {
        label: "TV4Nyheterna",
        time: "19:00",
        channel: "TV4"
    },
    {
        label: "Postkodmiljonären",
        time: "19:30",
        channel: "TV4"
    },
    {
        label: "Idol 2017",
        time: "06:00",
        channel: "TV4"
    },
    {
        label: "TV4Nyheterna och sport",
        time: "22:00",
        channel: "TV4"
    },
    {
        label: "Idol 2017: Röstningsprogram",
        time: "22:25",
        channel: "TV4"
    },
    {
        label: "Idol extra",
        time: "22:50",
        channel: "TV4"
    },
    {
        label: "Polisskolan 5",
        time: "23:20",
        channel: "TV4"
    },
    {
        label: "Gremlins 2: Det nya gänget",
        time: "01:10",
        channel: "TV4"
    },
    {
        label: "South Park: Bigger, longer & uncut",
        time: "03:20",
        channel: "TV4"
    }
];

$(".searchbox").autocomplete({
    source: tvListings    
});

/* Funktion för att visa meddelande om programmet som valts */
function showSearchResult() {
    var show = $('.searchbox').val();
    var showRes = show.toUpperCase();
    try {
        if (show === "") throw "är tom.";
        var i;
        for (i = 0; i < tvListings.length; i++) {
            if (show === tvListings[i].label) {
                if (tvListings[i].hasOwnProperty('time2') && tvListings[i].hasOwnProperty('channel2')) {
                    listing = showRes + "<br><br>Sänds på " + tvListings[i].channel + " klockan " + tvListings[i].time + ". <br>Programmet sänds även på " + tvListings[i].channel2 + 
                    " klockan " + tvListings[i].time2 + ".";                    
                } else if (tvListings[i].hasOwnProperty('time3')) {
                    listing = showRes + "<br><br>Sänds på " + tvListings[i].channel + " klockan " + tvListings[i].time + " samt " + tvListings[i].time2 + " & " + 
                    tvListings[i].time3 + ".";
                } else if (tvListings[i].hasOwnProperty('time2')) {
                    listing = showRes + "<br><br>Sänds på " + tvListings[i].channel + " klockan " + tvListings[i].time + " samt " + tvListings[i].time2 + ".";                    
                } else {
                    listing = showRes + "<br><br>Sänds på " + tvListings[i].channel + " klockan " + tvListings[i].time + ".";                                       
                }                        
                showOverlay(listing);             
            }                
        }
    } catch (error) {
        alert("Sökrutan " + error);    
    }
}