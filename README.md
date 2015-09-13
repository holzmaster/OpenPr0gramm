# OpenPr0gramm
Eine Quelloffene .NET-Implementierung für das pr0gramm.

## Installation

Via NuGet:
```
Install-Package OpenPr0gramm
```

## Verwendung
Die Library besteht aus 3 Schichten und ist an der JS-API der Webseite orientiert:
1. Refit-HTTP-Wrapper
2. Mapping der Rohdaten auf die Interface-Abstraktionen (IPr0grammApiClient)
3. Wrapping von abstrahierten Parametern auf die Rohdaten (Pr0grammClient)

Für den normalen Umgang sollte der oberste Layer reichen. Wenn du willst, kannst du aber auch die einzelnen Schichten austauschen.

```C#
var client = new Pr0grammClient();
var loginRes = await client.User.LogIn("user", "password");
if(!loginRes.Success)
{
	if(loginRes.Ban != null && loginRes.Ban.IsBanned)
	{
		Console.WriteLine($"Du bist bis {loginRes.Ban.Until} gebannt. Warum? \"{loginRes.Ban.Reason}\".");
	}
	else
	{
		Console.WriteLine("Das Passwort war wohl falsch oder so.");
	}
	return;
}
var frontItemRes = await client.Item.GetItems(ItemFlags.SFW, ItemStatus.Promoted);
Console.WriteLine("Posts:");
foreach(var item in frontItemRes.Items)
{
	Console.WriteLine($"{item.Id} von {item.User} ({item.Mark})");
}

CookieContainer container = client.GetCookie(); // Kann weggespeichert/serialisiert werden
// für spätere Verwendung (um sich nicht noch mal einloggen zu müssen)
var client2 = new Pr0grammClient(container); // Client mit Satz an Cookies initialisieren
```

## Nutzungsbestimmungen/Lizenz
Zusätzlich zu den in der LICENSE-File angegebenen Bestimmungen gilt:
- **Keine Kommerzielle Nutzung.**
- Hole dir *vorher* die Erlaubnis der Seitenbetreiber, deine Anwendung zu entwickeln.
Wenn du etwas vorhast, was nicht in Einklang mit den Nutzungsbestimmungen ist, kontaktiere mich (via pr0gramm/Email) und wir können drüber reden.

## Bugs
Es kann sein, dass bei der Serialisierung bestimmte Felder aufgrund von Typos oder Brainlags bei der Implementierung nicht richtig gemappt werden. Wenn dir sowas auffällt, kontaktire mich bitte, poste eine Issue oder fix es selber und stelle einen Pull-Request.
