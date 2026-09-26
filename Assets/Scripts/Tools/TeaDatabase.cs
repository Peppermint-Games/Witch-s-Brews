using System.Collections.Generic;

public class TeaDatabase
{
    public static Teabag newTea(int id)
    {
        Teabag newTea = new Teabag();

        newTea.id = id;
        newTea.growTime = 5;
        newTea.isPremade = true;

        switch (id)
        {
            // =========================================================
            // ENERGETIC
            // =========================================================

            case 0:
                newTea.teaName = "English Breakfast";
                newTea.description = "A strong and lively black tea blend for starting the day.";
                newTea.ingredients = new List<int> { 2, 3, 4, 5 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Energetic,
                            value = 6
                        },
                        new VibeValue
                        {
                            type = vibe.Chill,
                            value = 1
                        }
                    };
                newTea.teaVibe = vibe.Energetic;
                break;
            case 1:
                newTea.teaName = "Morning Spark";
                newTea.description = "Bright berries and brisk tea with a cool peppermint finish.";
                newTea.ingredients = new List<int> { 1, 2, 4, 6 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Energetic,
                            value = 4
                        }
                    };
                newTea.teaVibe = vibe.Energetic;
                break;
            // =========================================================
            // CHILL
            // =========================================================
            case 2:
                newTea.teaName = "Quiet Garden";
                newTea.description = "A gentle floral infusion made for slow and peaceful afternoons.";
                newTea.ingredients = new List<int> { 0, 7, 8, 9 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Chill,
                            value = 11
                        }
                    };
                newTea.teaVibe = vibe.Chill;
                break;
            case 3:
                newTea.teaName = "Afternoon Drift";
                newTea.description = "A mellow blend of soft tea, lavender, and lemon balm.";
                newTea.ingredients = new List<int> { 0, 3, 7, 8 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Chill,
                            value = 8
                        }
                    };
                newTea.teaVibe = vibe.Chill;
                break;
            // =========================================================
            // FOCUSED
            // =========================================================
            case 4:
                newTea.teaName = "Study Session";
                newTea.description = "A clean herbal blend for long books, longer notes, and looming deadlines.";
                newTea.ingredients = new List<int> { 10, 11, 12, 13 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Focused,
                            value = 11
                        }
                    };
                newTea.teaVibe = vibe.Focused;
                break;
            case 5:
                newTea.teaName = "Clear Head";
                newTea.description = "Fresh green tea and aromatic herbs sharpened with cool peppermint.";
                newTea.ingredients = new List<int> { 6, 10, 11, 12 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Focused,
                            value = 6
                        },
                        new VibeValue
                        {
                            type = vibe.Energetic,
                            value = 1
                        }
                    };

                newTea.teaVibe = vibe.Focused;
                break;
            // =========================================================
            // COMFORTING
            // =========================================================
            case 6:
                newTea.teaName = "Apple Pie";
                newTea.description = "Sweet apple, cinnamon, and vanilla in a warm rooibos base.";
                newTea.ingredients = new List<int> { 14, 15, 16, 17 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Comforting,
                            value = 12
                        }
                    };
                newTea.teaVibe = vibe.Comforting;
                break;
            case 7:
                newTea.teaName = "Rainy Window";
                newTea.description = "A mellow cup of apple, vanilla, chamomile, and rooibos for staying inside.";
                newTea.ingredients = new List<int> { 0, 14, 16, 17 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Comforting,
                            value = 8
                        },
                        new VibeValue
                        {
                            type = vibe.Chill,
                            value = 2
                        }
                    };
                newTea.teaVibe = vibe.Comforting;
                break;
            // =========================================================
            // HAPPY
            // =========================================================
            case 8:
                newTea.teaName = "Sunshine Punch";
                newTea.description = "A colourful mix of berries, citrus, and hibiscus bursting with fruit.";
                newTea.ingredients = new List<int> { 1, 18, 19, 20 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Happy,
                            value = 6
                        },
                        new VibeValue
                        {
                            type = vibe.Energetic,
                            value = 1
                        }
                    };
                newTea.teaVibe = vibe.Happy;
                break;
            case 9:
                newTea.teaName = "Good News";
                newTea.description = "Sweet strawberry, orange, and elderflower with a bright floral lift.";
                newTea.ingredients = new List<int> { 18, 19, 20, 21 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Happy,
                            value = 10
                        }
                    };

                newTea.teaVibe = vibe.Happy;
                break;
            // =========================================================
            // SLEEPY
            // =========================================================
            case 10:
                newTea.teaName = "Bedtime";
                newTea.description = "A deep herbal infusion for when the lights are low and the day is done.";
                newTea.ingredients = new List<int> { 22, 23, 24, 25 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Sleepy,
                            value = 13
                        }
                    };
                newTea.teaVibe = vibe.Sleepy;
                break;
            case 11:
                newTea.teaName = "Sweet Dreams";
                newTea.description = "Soft chamomile, lavender, linden flower, and passionflower for a quiet night.";
                newTea.ingredients = new List<int> { 0, 7, 23, 24 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Sleepy,
                            value = 7
                        },
                        new VibeValue
                        {
                            type = vibe.Chill,
                            value = 5
                        }
                    };
                newTea.teaVibe = vibe.Sleepy;
                break;
            // =========================================================
            // ROMANTIC
            // =========================================================
            case 12:
                newTea.teaName = "First Date";
                newTea.description = "Rose, jasmine, strawberry, and cacao for an evening worth remembering.";
                newTea.ingredients = new List<int> { 19, 26, 27, 29 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Romantic,
                            value = 11
                        },
                        new VibeValue
                        {
                            type = vibe.Happy,
                            value = 2
                        }
                    };
                newTea.teaVibe = vibe.Romantic;
                break;
            case 13:
                newTea.teaName = "Cherry Kiss";
                newTea.description = "Delicate blossoms and fragrant florals softened with sweet vanilla.";
                newTea.ingredients = new List<int> { 17, 26, 27, 28 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Romantic,
                            value = 11
                        },
                        new VibeValue
                        {
                            type = vibe.Comforting,
                            value = 5
                        }
                    };
                newTea.teaVibe = vibe.Romantic;
                break;
            // =========================================================
            // CREATIVE
            // =========================================================
            case 14:
                newTea.teaName = "Blue Canvas";
                newTea.description = "A vivid blue infusion with tropical fruit, basil, and aromatic spice.";
                newTea.ingredients = new List<int> { 30, 31, 32, 33 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Creative,
                            value = 13
                        }
                    };
                newTea.teaVibe = vibe.Creative;
                break;
            case 15:
                newTea.teaName = "Writer's Block";
                newTea.description = "An unusual blend of butterfly pea, green tea, basil, and star anise.";
                newTea.ingredients = new List<int> { 10, 30, 31, 33 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Creative,
                            value = 10
                        },
                        new VibeValue
                        {
                            type = vibe.Focused,
                            value = 2
                        }
                    };

                newTea.teaVibe = vibe.Creative;
                break;
            // =========================================================
            // CONFIDENT
            // =========================================================
            case 16:
                newTea.teaName = "Lionheart";
                newTea.description = "A fiery combination of ginger, cardamom, clove, and bold black tea.";
                newTea.ingredients = new List<int> { 2, 34, 35, 36 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Confident,
                            value = 11
                        },
                        new VibeValue
                        {
                            type = vibe.Energetic,
                            value = 1
                        }
                    };
                newTea.teaVibe = vibe.Confident;
                break;
            case 17:
                newTea.teaName = "Big Day";
                newTea.description = "Brisk Ceylon with warming ginger and spices for when hesitation isn't an option.";
                newTea.ingredients = new List<int> { 4, 34, 35, 36 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Confident,
                            value = 11
                        },
                        new VibeValue
                        {
                            type = vibe.Energetic,
                            value = 1
                        }
                    };

                newTea.teaVibe = vibe.Confident;
                break;
            // =========================================================
            // REFRESHED
            // =========================================================
            case 18:
                newTea.teaName = "Garden Cooler";
                newTea.description = "Cucumber, lemon, lemongrass, and peppermint for a crisp garden-fresh cup.";
                newTea.ingredients = new List<int> { 6, 37, 38, 39 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Refreshed,
                            value = 4
                        },
                        new VibeValue
                        {
                            type = vibe.Energetic,
                            value = 1
                        }
                    };
                newTea.teaVibe = vibe.Refreshed;
                break;
            case 19:
                newTea.teaName = "Fresh Start";
                newTea.description = "Bright lemon and crisp cucumber lifted by green tea and lemongrass.";
                newTea.ingredients = new List<int> { 10, 37, 38, 39 };
                newTea.vibes = new List<VibeValue>
                    {
                        new VibeValue
                        {
                            type = vibe.Refreshed,
                            value = 4
                        },
                        new VibeValue
                        {
                            type = vibe.Focused,
                            value = 2
                        }
                    };
                newTea.teaVibe = vibe.Refreshed;
                break;
        }
        return newTea;
    }
}