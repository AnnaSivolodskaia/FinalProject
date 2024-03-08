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
            case "Cut_Scene_1":
                finalText = "Hey there, stranger! Welcome to our little slice of the woods. I'm Beaver the Mayor and I've heard you're new in town. Figured I'd come say hi and give you the lowdown on what's happening.\r\n\r\nNow, I'll be straight with you – things haven't been going as smoothly as I'd hoped lately. I made some promises to the citizens, and well, I haven't exactly delivered on all of them. I’ll be re-electing soon and, you know, there is something I need to do with my ratings. And hey, that's where you come in.\r\n\r\nYou see, you're kind of a fresh set of eyes around here, and I'm thinking you might have some ideas on how we can fix things up. I know you're not a local, and you won't be casting any votes, but your input could make a real difference.\r\n\r\nSo, what do you say? Help the Mayor out, and let's make this village the best it can be!\r\n";
                break;
            case "Cut_Scene_2":
                finalText = "That's awesome of you to lend a paw! So, here's the deal – I made this promise to the locals that our village streets would be kept tidy. You know, regular clean-ups and all. Plus, I threw in this recycling twist to make things greener.\r\n\r\nThing is, I haven't quite lived up to my end of the bargain, and the streets are getting messier by the day. People are still tossing their trash around like it's a game of catch.\r\n\r\nI could really use your help in picking up all this mess and sorting it out for recycling: we separate organic (‘Z’ key), plastic (‘X’ key) and metal (‘C’ key). Walk around the street (left and right arrows), and collect whatever you find.\r\n\r\nQuick heads up: if trash stays too long, locals pick it. And our recycling workers prefer it sorted right. It's a bit messy, but with your hand, we can make this place cleaner.\r\n\r\nReady to start?\r\n";
                break;
            case "1lvl_3":
                finalText = "Hey, appreciate your effort in tackling the trash situation, but it seems we hit a little snag. The locals got a bit too eager and cleaned up some of the mess themselves, and the recycling workers weren't too happy with the sorting.\r\n\r\nI know, it's a bit tricky, but I'm confident we can get it right this time. Let's give it another shot – grab a bag, use 'Z' for organic, 'X' for plastic, 'C' for metal. Walk around, collect, but keep an eye on the locals and make sure things are sorted just right. We got this – ready to give it another go?";
                break;
            case "1lvl_2":
                finalText = "Hey, big shoutout to you, my friend! Thanks to your help, our streets are looking squeaky clean now. I can't thank you enough for rolling up your sleeves and diving into the mess with us.\r\n\r\nThe village is already feeling fresher and more inviting, all thanks to your hard work. It's amazing what we can achieve when we team up, isn't it?\r\n";
                break;
            case "Cut_Scene_3":
                finalText = "Now, I've got another tiny favor to ask – fresh fish distribution time! \r\n\r\nGrab a box (with 'C' key), give it to each citizen (with 'C' key again). Watch out for hungry folks asking for a second box, ask them to leave (hit 'X' key). \r\n\r\nAlso, keep an eye on their emotions: hunger can make folks a tad impatient. Let's make sure citizens don’t leave disappointed!\r\n\r\nReady to get those fish shared?\r\n";
                break;
            case "2lvl_2":
                finalText = "Hey again, traveler! Big thanks for your help with the fish distribution – it went well, and citizens are happy. As we gear up for the election, I wanted to express my gratitude. You've been a key player in turning things around. Fingers crossed for the votes, and wish me luck! \r\n\r\nLet’s go and find out the results! \r\n";
                break;
            case "2lvl_3":
                finalText = "Hey again, traveler! We hit a snag with the fish distribution – lots left disappointed. Can we try again? \r\n\r\nGrab a box (with 'C' key), give it to citizens (with 'C' key again). If someone aims for a second box, ask them to leave (press 'X' key).\r\nWatch emotions: hungry folks need their fair share. \r\n\r\nReady for another round? Your help is crucial!\r\n";
                break;
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
            case "Cut_Scene_1":
                finalText = "Press C: \"I'm in! Where do I start?\" ";
                break;
            case "Cut_Scene_2":
                finalText = "Press C: start the game";
                break;
            case "1lvl_3":
                finalText = "Press C: try again";
                break;
            case "1lvl_2":
                finalText = "Press C: \"We got this!\" ";
                break;
            case "Cut_Scene_3":
                finalText = "Press C: start the game";
                break;
            case "2lvl_2":
                finalText = "Press C: \"Let's find it out!\"";
                break;
            case "2lvl_3":
                finalText = "Press C: try again";
                break;
            default:
                finalText = "Not defined in catalog!";
                break;
        }
        return finalText;
    }

    public static string FindCutSceneVoiceOver(string currentGameState)
    {
        string finalText;
        switch (currentGameState)
        {
            case "Cut_Scene_1":
                finalText = "Intro1";
                break;
            case "Cut_Scene_2":
                finalText = "Intro2";
                break;
            case "1lvl_3":
                finalText = "FirstLevelFailed";
                break;
            case "1lvl_2":
                finalText = "FirstLevelSuccessed";
                break;
            case "Cut_Scene_3":
                finalText = "Intro3";
                break;
            case "2lvl_2":
                finalText = "SecondLevelSuccessed";
                break;
            case "2lvl_3":
                finalText = "SecondLevelFailed";
                break;
            default:
                finalText = "";
                break;
        }
        return finalText;
    }
}
