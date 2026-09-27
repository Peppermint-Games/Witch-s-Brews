using System.Collections.Generic;

public static class KeywordDatabase
{
    public static List<OrderKeyword> BuildDatabase()
    {
        List<OrderKeyword> keywords =
            new List<OrderKeyword>();

        // =====================================================
        // ENERGETIC
        // =====================================================

        Add(keywords, "Something to wake me up.", vibe.Energetic, 3);
        Add(keywords, "I need a bit of a boost.", vibe.Energetic, 2);
        Add(keywords, "Something with a little kick, please.", vibe.Energetic, 2);
        Add(keywords, "I need to get moving.", vibe.Energetic, 2);
        Add(keywords, "Something to put some spring in my step.", vibe.Energetic, 2);
        Add(keywords, "I'm absolutely exhausted.", vibe.Energetic, 3);
        Add(keywords, "I could use some energy.", vibe.Energetic, 3);
        Add(keywords, "Something bright and lively.", vibe.Energetic, 2);
        Add(keywords, "I've got a long day ahead of me.", vibe.Energetic, 2);
        Add(keywords, "I need something to perk me up.", vibe.Energetic, 2);
        Add(keywords, "I feel like I could fall asleep standing up.", vibe.Energetic, 3);
        Add(keywords, "Give me something that'll get me going.", vibe.Energetic, 3);
        Add(keywords, "I've still got hours to go today.", vibe.Energetic, 2);
        Add(keywords, "I need something with some life to it.", vibe.Energetic, 2);
        Add(keywords, "It's way too early for me.", vibe.Energetic, 3);
        Add(keywords, "I need to put a little pep in my step.", vibe.Energetic, 2);
        Add(keywords, "Something that'll get me up and running.", vibe.Energetic, 3);
        Add(keywords, "I barely slept last night.", vibe.Energetic, 3);
        Add(keywords, "I need to recharge my batteries.", vibe.Energetic, 2);
        Add(keywords, "Give me something lively.", vibe.Energetic, 2);

        // =====================================================
        // CHILL
        // =====================================================

        Add(keywords, "Something nice and relaxing.", vibe.Chill, 3);
        Add(keywords, "I'd like to slow down for a while.", vibe.Chill, 2);
        Add(keywords, "Something peaceful, please.", vibe.Chill, 3);
        Add(keywords, "I've had a pretty hectic day.", vibe.Chill, 2);
        Add(keywords, "Something to help me unwind.", vibe.Chill, 3);
        Add(keywords, "I'd like something mellow.", vibe.Chill, 2);
        Add(keywords, "Nothing too intense.", vibe.Chill, 1);
        Add(keywords, "I just want to take it easy.", vibe.Chill, 2);
        Add(keywords, "Something calm would be lovely.", vibe.Chill, 3);
        Add(keywords, "I need a quiet moment.", vibe.Chill, 2);
        Add(keywords, "I finally have nowhere to be.", vibe.Chill, 2);
        Add(keywords, "Something for watching the world go by.", vibe.Chill, 2);
        Add(keywords, "I want to forget about the clock for a while.", vibe.Chill, 3);
        Add(keywords, "Today's been a little too loud.", vibe.Chill, 3);
        Add(keywords, "I just want to sit here for a bit.", vibe.Chill, 2);
        Add(keywords, "Something easy-going would be perfect.", vibe.Chill, 2);
        Add(keywords, "I need to decompress.", vibe.Chill, 3);
        Add(keywords, "Can I get something nice and mellow?", vibe.Chill, 2);
        Add(keywords, "I'm taking the afternoon off.", vibe.Chill, 2);
        Add(keywords, "Give me something for doing absolutely nothing.", vibe.Chill, 2);

        // =====================================================
        // FOCUSED
        // =====================================================

        Add(keywords, "I need to concentrate.", vibe.Focused, 3);
        Add(keywords, "I've got some studying to do.", vibe.Focused, 3);
        Add(keywords, "Something to clear my head.", vibe.Focused, 2);
        Add(keywords, "I need to get some work done.", vibe.Focused, 2);
        Add(keywords, "Something to keep me on task.", vibe.Focused, 3);
        Add(keywords, "I've got a deadline coming up.", vibe.Focused, 2);
        Add(keywords, "My mind's all over the place today.", vibe.Focused, 2);
        Add(keywords, "Something for a productive afternoon.", vibe.Focused, 2);
        Add(keywords, "I could use a little clarity.", vibe.Focused, 2);
        Add(keywords, "Help me get my thoughts in order.", vibe.Focused, 3);
        Add(keywords, "I've got a mountain of paperwork waiting for me.", vibe.Focused, 3);
        Add(keywords, "I need to keep my mind sharp.", vibe.Focused, 3);
        Add(keywords, "There's a lot I need to get through today.", vibe.Focused, 2);
        Add(keywords, "I keep getting distracted.", vibe.Focused, 3);
        Add(keywords, "Something to help me think straight.", vibe.Focused, 3);
        Add(keywords, "I need to finish this chapter tonight.", vibe.Focused, 2);
        Add(keywords, "I've got an exam coming up.", vibe.Focused, 3);
        Add(keywords, "Something for a long study session.", vibe.Focused, 3);
        Add(keywords, "I need to get back into the zone.", vibe.Focused, 3);
        Add(keywords, "My brain could use a little organisation.", vibe.Focused, 2);

        // =====================================================
        // COMFORTING
        // =====================================================

        Add(keywords, "Something warm and comforting.", vibe.Comforting, 3);
        Add(keywords, "I've had a rough day.", vibe.Comforting, 3);
        Add(keywords, "Something cosy, please.", vibe.Comforting, 3);
        Add(keywords, "I could use a little comfort.", vibe.Comforting, 3);
        Add(keywords, "Something that feels like home.", vibe.Comforting, 2);
        Add(keywords, "I need cheering up.", vibe.Comforting, 2);
        Add(keywords, "Something warm and familiar.", vibe.Comforting, 2);
        Add(keywords, "It's been one of those days.", vibe.Comforting, 2);
        Add(keywords, "I'd like something soothing.", vibe.Comforting, 2);
        Add(keywords, "Give me something for a rainy afternoon.", vibe.Comforting, 2);
        Add(keywords, "I think I need a hug in a cup.", vibe.Comforting, 3);
        Add(keywords, "It's miserable outside today.", vibe.Comforting, 2);
        Add(keywords, "Give me something cosy enough for a blanket.", vibe.Comforting, 3);
        Add(keywords, "I'm feeling a little homesick.", vibe.Comforting, 3);
        Add(keywords, "Something soft and familiar sounds nice.", vibe.Comforting, 2);
        Add(keywords, "Today didn't exactly go to plan.", vibe.Comforting, 3);
        Add(keywords, "I need a little pick-me-up.", vibe.Comforting, 2);
        Add(keywords, "Something to make everything feel okay.", vibe.Comforting, 3);
        Add(keywords, "I could really use some kindness today.", vibe.Comforting, 3);
        Add(keywords, "Give me the tea equivalent of a warm blanket.", vibe.Comforting, 3);

        // =====================================================
        // HAPPY
        // =====================================================

        Add(keywords, "Something cheerful!", vibe.Happy, 3);
        Add(keywords, "I'm in the mood for something fun.", vibe.Happy, 2);
        Add(keywords, "Something to brighten my day.", vibe.Happy, 3);
        Add(keywords, "I'd like something sweet and cheerful.", vibe.Happy, 2);
        Add(keywords, "Something that puts a smile on my face.", vibe.Happy, 3);
        Add(keywords, "I'm celebrating today!", vibe.Happy, 3);
        Add(keywords, "Give me something sunny.", vibe.Happy, 2);
        Add(keywords, "I'm in a great mood today.", vibe.Happy, 2);
        Add(keywords, "Something light-hearted, please.", vibe.Happy, 2);
        Add(keywords, "Surprise me with something joyful.", vibe.Happy, 3);
        Add(keywords, "Today's been fantastic!", vibe.Happy, 3);
        Add(keywords, "Give me something that feels like sunshine.", vibe.Happy, 3);
        Add(keywords, "I'm having a surprisingly good day.", vibe.Happy, 2);
        Add(keywords, "Something fun and colourful sounds perfect.", vibe.Happy, 2);
        Add(keywords, "I got some great news today!", vibe.Happy, 3);
        Add(keywords, "Let's have something festive.", vibe.Happy, 2);
        Add(keywords, "I'm treating myself today.", vibe.Happy, 2);
        Add(keywords, "Give me the happiest thing you've got.", vibe.Happy, 3);
        Add(keywords, "Something bubbly in spirit, if not literally.", vibe.Happy, 2);
        Add(keywords, "I feel like celebrating the little things.", vibe.Happy, 2);

        // =====================================================
        // SLEEPY
        // =====================================================

        Add(keywords, "Something to help me sleep.", vibe.Sleepy, 3);
        Add(keywords, "I'm heading to bed soon.", vibe.Sleepy, 3);
        Add(keywords, "Something for a quiet night.", vibe.Sleepy, 2);
        Add(keywords, "I'd like something before bed.", vibe.Sleepy, 3);
        Add(keywords, "I can't seem to switch off tonight.", vibe.Sleepy, 3);
        Add(keywords, "Something gentle and sleepy.", vibe.Sleepy, 3);
        Add(keywords, "I need an early night.", vibe.Sleepy, 2);
        Add(keywords, "Something to help me drift off.", vibe.Sleepy, 3);
        Add(keywords, "I'm ready to call it a day.", vibe.Sleepy, 2);
        Add(keywords, "I'd like something for bedtime.", vibe.Sleepy, 3);
        Add(keywords, "I want to be asleep the moment I get home.", vibe.Sleepy, 3);
        Add(keywords, "It's nearly time to turn in.", vibe.Sleepy, 2);
        Add(keywords, "My brain won't stop thinking tonight.", vibe.Sleepy, 3);
        Add(keywords, "Something quiet for the end of the day.", vibe.Sleepy, 2);
        Add(keywords, "I'd like to wind down before bed.", vibe.Sleepy, 3);
        Add(keywords, "I've been tossing and turning lately.", vibe.Sleepy, 3);
        Add(keywords, "Give me something for a peaceful night.", vibe.Sleepy, 3);
        Add(keywords, "I want to curl up and fall asleep.", vibe.Sleepy, 2);
        Add(keywords, "Something gentle enough for midnight.", vibe.Sleepy, 2);
        Add(keywords, "I'm hoping for an early night tonight.", vibe.Sleepy, 3);

        // =====================================================
        // ROMANTIC
        // =====================================================

        Add(keywords, "Something romantic, please.", vibe.Romantic, 3);
        Add(keywords, "I've got a date tonight.", vibe.Romantic, 3);
        Add(keywords, "Something a little dreamy.", vibe.Romantic, 2);
        Add(keywords, "I'd like something sweet and floral.", vibe.Romantic, 2);
        Add(keywords, "Something for two, if you know what I mean.", vibe.Romantic, 2);
        Add(keywords, "I'm meeting someone special later.", vibe.Romantic, 3);
        Add(keywords, "Something with a little romance.", vibe.Romantic, 3);
        Add(keywords, "Give me something charming.", vibe.Romantic, 2);
        Add(keywords, "I'm feeling a little lovestruck.", vibe.Romantic, 3);
        Add(keywords, "Something for a perfect evening.", vibe.Romantic, 2);
        Add(keywords, "It's our anniversary tonight.", vibe.Romantic, 3);
        Add(keywords, "Something that feels like candlelight.", vibe.Romantic, 3);
        Add(keywords, "I might be trying to impress someone.", vibe.Romantic, 2);
        Add(keywords, "Give me something a little enchanting.", vibe.Romantic, 2);
        Add(keywords, "I've got butterflies today.", vibe.Romantic, 3);
        Add(keywords, "Something sweet for someone special.", vibe.Romantic, 3);
        Add(keywords, "I'm planning a romantic evening.", vibe.Romantic, 3);
        Add(keywords, "Something delicate and lovely, please.", vibe.Romantic, 2);
        Add(keywords, "I think I might be falling for someone.", vibe.Romantic, 3);
        Add(keywords, "Give me something worthy of a love letter.", vibe.Romantic, 3);

        // =====================================================
        // CREATIVE
        // =====================================================

        Add(keywords, "I need some inspiration.", vibe.Creative, 3);
        Add(keywords, "Something to get the ideas flowing.", vibe.Creative, 3);
        Add(keywords, "I've got a bit of writer's block.", vibe.Creative, 3);
        Add(keywords, "Something unusual, please.", vibe.Creative, 2);
        Add(keywords, "I feel like making something today.", vibe.Creative, 2);
        Add(keywords, "Something to spark my imagination.", vibe.Creative, 3);
        Add(keywords, "I'm working on a new project.", vibe.Creative, 2);
        Add(keywords, "Give me something inspiring.", vibe.Creative, 3);
        Add(keywords, "I need a fresh perspective.", vibe.Creative, 2);
        Add(keywords, "Something to get me out of a creative rut.", vibe.Creative, 3);
        Add(keywords, "I've got an empty page staring at me.", vibe.Creative, 3);
        Add(keywords, "I need to shake loose a few ideas.", vibe.Creative, 3);
        Add(keywords, "Something to get my imagination moving.", vibe.Creative, 3);
        Add(keywords, "I've been staring at the same sketch for hours.", vibe.Creative, 3);
        Add(keywords, "Give me something a little weird.", vibe.Creative, 2);
        Add(keywords, "I want to make something new today.", vibe.Creative, 2);
        Add(keywords, "My muse seems to have taken the day off.", vibe.Creative, 3);
        Add(keywords, "Something to help me think outside the box.", vibe.Creative, 3);
        Add(keywords, "I've got half an idea and need the other half.", vibe.Creative, 3);
        Add(keywords, "Surprise me with something imaginative.", vibe.Creative, 2);

        // =====================================================
        // CONFIDENT
        // =====================================================

        Add(keywords, "I need a little courage.", vibe.Confident, 3);
        Add(keywords, "I've got a big day ahead.", vibe.Confident, 2);
        Add(keywords, "Something to make me feel unstoppable.", vibe.Confident, 3);
        Add(keywords, "I could use a confidence boost.", vibe.Confident, 3);
        Add(keywords, "I've got an important meeting later.", vibe.Confident, 2);
        Add(keywords, "Something bold, please.", vibe.Confident, 2);
        Add(keywords, "I need to psych myself up.", vibe.Confident, 3);
        Add(keywords, "Something to steady my nerves.", vibe.Confident, 2);
        Add(keywords, "Today's the day. Give me something strong.", vibe.Confident, 3);
        Add(keywords, "I need to feel a little braver.", vibe.Confident, 3);
        Add(keywords, "I've got a presentation in an hour.", vibe.Confident, 3);
        Add(keywords, "I need something to settle these butterflies.", vibe.Confident, 2);
        Add(keywords, "Give me something that says I've got this.", vibe.Confident, 3);
        Add(keywords, "I need to walk in there like I own the place.", vibe.Confident, 3);
        Add(keywords, "There's something important I need to do today.", vibe.Confident, 2);
        Add(keywords, "I could use a little backbone.", vibe.Confident, 3);
        Add(keywords, "Something bold enough for a big decision.", vibe.Confident, 3);
        Add(keywords, "I'm about to take a bit of a leap.", vibe.Confident, 2);
        Add(keywords, "I need to stop second-guessing myself.", vibe.Confident, 3);
        Add(keywords, "Give me something for facing the day head-on.", vibe.Confident, 3);

        // =====================================================
        // REFRESHED
        // =====================================================

        Add(keywords, "Something fresh, please.", vibe.Refreshed, 3);
        Add(keywords, "I'd like something crisp and refreshing.", vibe.Refreshed, 3);
        Add(keywords, "It's been such a hot day.", vibe.Refreshed, 2);
        Add(keywords, "Something to cool me down.", vibe.Refreshed, 3);
        Add(keywords, "I need a fresh start.", vibe.Refreshed, 3);
        Add(keywords, "Something clean and light.", vibe.Refreshed, 2);
        Add(keywords, "I'd like something invigorating.", vibe.Refreshed, 2);
        Add(keywords, "Something for after a long walk.", vibe.Refreshed, 2);
        Add(keywords, "I need to feel refreshed.", vibe.Refreshed, 3);
        Add(keywords, "Give me something bright and fresh.", vibe.Refreshed, 2);
        Add(keywords, "I've been out in the sun all afternoon.", vibe.Refreshed, 3);
        Add(keywords, "Something cool and clean sounds wonderful.", vibe.Refreshed, 3);
        Add(keywords, "I just got back from a long walk.", vibe.Refreshed, 2);
        Add(keywords, "I need something to clear away the cobwebs.", vibe.Refreshed, 2);
        Add(keywords, "Give me something that tastes like a fresh breeze.", vibe.Refreshed, 3);
        Add(keywords, "Something light would be perfect.", vibe.Refreshed, 2);
        Add(keywords, "I feel like I need a reset.", vibe.Refreshed, 3);
        Add(keywords, "It's far too warm today.", vibe.Refreshed, 3);
        Add(keywords, "Something crisp sounds really good right now.", vibe.Refreshed, 3);
        Add(keywords, "I'd like something that feels like a new morning.", vibe.Refreshed, 2);

        return keywords;
    }

    private static void Add(
        List<OrderKeyword> database,
        string text,
        vibe targetVibe,
        int weight)
    {
        database.Add(
            new OrderKeyword
            {
                text = text,
                targetVibe = targetVibe,
                weight = weight
            }
        );
    }
}