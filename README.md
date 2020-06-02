# mooi-deurbel-ding

Een mooi schoolproject voor IoT, domotica

# Opdacht A

Alle commands staan (onderaan) in ArduinoHandler. Elke command heeft zijn eigen code voor veiligheid, minder kans op bugs, en als er bugs zijn zijn ze makkelijker te debuggen. 

Er zijn bijna overal comments, dus ik raad aan om ze te lezen. **Lees de comments bij het Commands gedeelte heel nauwkeurig.**
De GUI wordt geüpdate door een binding met ArduinoHandler's Status object. Als daarin een variabele wordt verandert wordt de GUI ook automatisch geüpdate. 

**Commands** kunnen elke lengte zijn (op dit moment max 15 maar dat is instelbaar, en eindigen op ">" zodat de Arduino weet wanneer de laatste character it.

**Arduino server responses** zijn altijd 4 characters en de laatste character is altijd "\n", want zo was de voorbeeld code...

Klasse diagram met nog wat uitleg: 

![Image](https://i.imgur.com/aiRzMGL.png)

![Commands](https://i.imgur.com/3VS7aoq.png)
