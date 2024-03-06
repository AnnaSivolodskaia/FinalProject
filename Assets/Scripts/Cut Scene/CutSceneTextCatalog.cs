using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScoreSystem;

public class CutSceneTextCatalog : MonoBehaviour
{
    public static string FindDialogMainText(string currentGameState)
	{
        string finalText;
        switch (currentGameState)
        {
            case "Intro_1":
                finalText = "Hey there, stranger! Welcome to our little slice of the woods. I'm Beaver the Mayor and I've heard you're new in town. Figured I'd come say hi and give you the lowdown on what's happening.\r\n\r\nNow, I'll be straight with you – things haven't been going as smoothly as I'd hoped lately. I made some promises to the citizens, and well, I haven't exactly delivered on all of them. I’ll be re-electing soon and, you know, there is something I need to do with my ratings. And hey, that's where you come in.\r\n\r\nYou see, you're kind of a fresh set of eyes around here, and I'm thinking you might have some ideas on how we can fix things up. I know you're not a local, and you won't be casting any votes, but your input could make a real difference.\r\n\r\nSo, what do you say? Help the Mayor out, and let's make this village the best it can be!\r\n";
                break;
            case "Intro_2":
                finalText = "That's awesome of you to lend a paw! So, here's the deal – I made this promise to the locals that our village streets would be kept tidy. You know, regular clean-ups and all. Plus, I threw in this recycling twist to make things greener.\r\n\r\nThing is, I haven't quite lived up to my end of the bargain, and the streets are getting messier by the day. People are still tossing their trash around like it's a game of catch.\r\n\r\nI could really use your help in picking up all this mess and sorting it out for recycling: we separate organic (‘Z’ key), plastic (‘X’ key) and metal (‘C’ key). Walk around the street (left and right arrows), and collect whatever you find.\r\n\r\nQuick heads up: if trash stays too long, locals pick it. And our recycling workers prefer it sorted right. It's a bit messy, but with your hand, we can make this place cleaner.\r\n\r\nReady to start?\r\n";
                break;
            case "1lvl_3":
                finalText = "Hey, appreciate your effort in tackling the trash situation, but it seems we hit a little snag. The locals got a bit too eager and cleaned up some of the mess themselves, and the recycling workers weren't too happy with the sorting.\r\n\r\nI know, it's a bit tricky, but I'm confident we can get it right this time. Let's give it another shot – grab a bag, use 'Z' for organic, 'X' for plastic, 'C' for metal. Walk around, collect, but keep an eye on the locals and make sure things are sorted just right. We got this – ready to give it another go?";
                break;
            case "1lvl_2":
                finalText = "Hey, big shoutout to you, my friend! Thanks to your help, our streets are looking squeaky clean now. I can't thank you enough for rolling up your sleeves and diving into the mess with us.\r\n\r\nThe village is already feeling fresher and more inviting, all thanks to your hard work. It's amazing what we can achieve when we team up, isn't it?\r\n";
                break;
            /*case "2lvl_1":
                finalText = "Hey there, traveler! Thanks a ton for sticking around, your help means the world to me. Now, I've got another tiny favor to ask. I made a promise to the locals about some fresh fish, and we've got this sizable shipment waiting to be shared.\r\nIf you could spare a moment and lend a paw to get these fish to each citizen, it would be fantastic. Simply grab a box with 'C' and deliver it to them by hitting 'C' again.\r\nBut here's the thing – a few folks might be feeling extra hungry and ask for a second box. If that comes up, just press 'X' and kindly let them know we're sticking to one box per person. We want to make sure everyone gets a fair share.\r\nAlso, keep an eye on their emotions; hunger can make folks a tad impatient. Let's make sure no one leaves disappointed!\r\nI know it's a bit of a hustle, but your support will ensure that every villager gets to enjoy those delicious fresh fish. Ready to dive into this fishy business together?\r\n"
                break;*/
            default:
                finalText = "Not defined in catalog!";
                break;
        }
        return finalText;
    }

    public static string FindDialogBottomText(string currentGameState)
    {
        string finalText;
        switch (currentGameState)
        {
            case "Intro_1":
                finalText = "Press C: \"I'm in! Where do I start?\" ";
                break;
            case "Intro_2":
                finalText = "Press C: start the game";
                break;
            case "1lvl_3":
                finalText = "Press C: try again";
                break;
            case "1lvl_2":
                int currentScore = ScoreSystem.GetScore();
                finalText = string.Format("Current score is: {0}/300.\r\n To be continued.....", currentScore);
                break;
            default:
                finalText = "Not defined in catalog!";
                break;
        }
        return finalText;
    }
}
