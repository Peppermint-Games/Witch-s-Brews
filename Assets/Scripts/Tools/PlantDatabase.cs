public class PlantDatabase
{
    public static PlantData newPlant(int id)
    {
        PlantData newPlant = new PlantData();
        newPlant.id = id;
        newPlant.yield = 1;
        newPlant.value = 1;
        newPlant.growTime = 1;
        newPlant.scale = timeScale.day;

        switch (id)
        {
            // =========================================================
            // EXISTING / STARTING PLANTS
            // =========================================================

            case 0:
                newPlant.plantName = "Chamomile";
                newPlant.description = "Floral, soft, and pleasantly mellow.";
                newPlant.growTime = 3;
                newPlant.vibe = vibe.Chill;
                newPlant.value = 2;
                break;

            case 1:
                newPlant.plantName = "Blueberries";
                newPlant.description = "Sweet little berries with a bright, fruity flavour.";
                newPlant.growTime = 5;
                newPlant.yield = 3;
                newPlant.vibe = vibe.Energetic;
                break;

            case 2:
                newPlant.plantName = "Black Tea";
                newPlant.description = "A bold and dependable base for a strong cup.";
                newPlant.growTime = 1;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Energetic;
                break;

            case 3:
                newPlant.plantName = "Assam";
                newPlant.description = "Rich and malty, with a deep and soothing body.";
                newPlant.growTime = 2;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Chill;
                break;

            case 4:
                newPlant.plantName = "Ceylon";
                newPlant.description = "A brisk and lively tea with a clean finish.";
                newPlant.growTime = 2;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Energetic;
                break;

            case 5:
                newPlant.plantName = "Keemun";
                newPlant.description = "A deep Chinese black tea with a rich aromatic character.";
                newPlant.growTime = 1;
                newPlant.vibe = vibe.Energetic;
                newPlant.value = 2;
                break;

            case 6:
                newPlant.plantName = "Peppermint";
                newPlant.description = "Cool, sharp, and wonderfully refreshing.";
                newPlant.growTime = 2;
                newPlant.yield = 3;
                newPlant.vibe = vibe.Energetic;
                break;


            // =========================================================
            // CHILL
            // =========================================================

            case 7:
                newPlant.plantName = "Lavender";
                newPlant.description = "A fragrant purple flower with a soft floral aroma.";
                newPlant.growTime = 4;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Chill;
                newPlant.value = 3;
                break;

            case 8:
                newPlant.plantName = "Lemon Balm";
                newPlant.description = "A gentle herb with a mellow lemon fragrance.";
                newPlant.growTime = 3;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Chill;
                newPlant.value = 2;
                break;

            case 9:
                newPlant.plantName = "White Tea";
                newPlant.description = "Delicate young tea leaves with a subtle, graceful flavour.";
                newPlant.growTime = 6;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Chill;
                newPlant.value = 4;
                break;


            // =========================================================
            // FOCUSED
            // =========================================================

            case 10:
                newPlant.plantName = "Green Tea";
                newPlant.description = "Fresh and grassy, with a clean and precise finish.";
                newPlant.growTime = 3;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Focused;
                newPlant.value = 2;
                break;

            case 11:
                newPlant.plantName = "Rosemary";
                newPlant.description = "A woody aromatic herb with a strong, distinctive character.";
                newPlant.growTime = 4;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Focused;
                newPlant.value = 2;
                break;

            case 12:
                newPlant.plantName = "Sage";
                newPlant.description = "Earthy and herbal with a warm, savoury edge.";
                newPlant.growTime = 4;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Focused;
                newPlant.value = 2;
                break;

            case 13:
                newPlant.plantName = "Ginseng";
                newPlant.description = "A prized earthy root with a powerful lingering character.";
                newPlant.growTime = 9;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Focused;
                newPlant.value = 5;
                break;


            // =========================================================
            // COMFORTING
            // =========================================================

            case 14:
                newPlant.plantName = "Rooibos";
                newPlant.description = "Smooth, earthy, and naturally mellow.";
                newPlant.growTime = 3;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Comforting;
                newPlant.value = 2;
                break;

            case 15:
                newPlant.plantName = "Cinnamon";
                newPlant.description = "Sweet and warming spice with a familiar cosy aroma.";
                newPlant.growTime = 6;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Comforting;
                newPlant.value = 4;
                break;

            case 16:
                newPlant.plantName = "Apple";
                newPlant.description = "Sweet, familiar fruit with a gentle autumn flavour.";
                newPlant.growTime = 5;
                newPlant.yield = 3;
                newPlant.vibe = vibe.Comforting;
                break;

            case 17:
                newPlant.plantName = "Vanilla";
                newPlant.description = "A rich and creamy sweetness that softens almost any blend.";
                newPlant.growTime = 8;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Comforting;
                newPlant.value = 5;
                break;


            // =========================================================
            // HAPPY
            // =========================================================

            case 18:
                newPlant.plantName = "Hibiscus";
                newPlant.description = "A vivid crimson flower with a bright, pleasantly tart taste.";
                newPlant.growTime = 4;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Happy;
                newPlant.value = 2;
                break;

            case 19:
                newPlant.plantName = "Strawberry";
                newPlant.description = "Juicy, sweet, and cheerfully fruity.";
                newPlant.growTime = 4;
                newPlant.yield = 3;
                newPlant.vibe = vibe.Happy;
                newPlant.value = 2;
                break;

            case 20:
                newPlant.plantName = "Orange";
                newPlant.description = "Bright citrus with a sweet and sunny fragrance.";
                newPlant.growTime = 5;
                newPlant.yield = 3;
                newPlant.vibe = vibe.Happy;
                newPlant.value = 2;
                break;

            case 21:
                newPlant.plantName = "Elderflower";
                newPlant.description = "Delicate blossoms with a light, sweet floral character.";
                newPlant.growTime = 6;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Happy;
                newPlant.value = 4;
                break;


            // =========================================================
            // SLEEPY
            // =========================================================

            case 22:
                newPlant.plantName = "Valerian";
                newPlant.description = "An earthy root with a heavy and distinctly herbal character.";
                newPlant.growTime = 7;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Sleepy;
                newPlant.value = 4;
                break;

            case 23:
                newPlant.plantName = "Passionflower";
                newPlant.description = "A delicate climbing flower with a mild grassy flavour.";
                newPlant.growTime = 6;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Sleepy;
                newPlant.value = 4;
                break;

            case 24:
                newPlant.plantName = "Linden Flower";
                newPlant.description = "Soft, sweet blossoms with a gentle honey-like fragrance.";
                newPlant.growTime = 5;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Sleepy;
                newPlant.value = 3;
                break;

            case 25:
                newPlant.plantName = "Hops";
                newPlant.description = "A distinctive flower with a deep, pleasantly bitter finish.";
                newPlant.growTime = 4;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Sleepy;
                newPlant.value = 2;
                break;


            // =========================================================
            // ROMANTIC
            // =========================================================

            case 26:
                newPlant.plantName = "Rose";
                newPlant.description = "Fragrant petals with an elegant and unmistakable floral sweetness.";
                newPlant.growTime = 5;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Romantic;
                newPlant.value = 3;
                break;

            case 27:
                newPlant.plantName = "Jasmine";
                newPlant.description = "A richly perfumed flower with a delicate lingering sweetness.";
                newPlant.growTime = 5;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Romantic;
                newPlant.value = 4;
                break;

            case 28:
                newPlant.plantName = "Cherry Blossom";
                newPlant.description = "Delicate pink blossoms with a light and fleeting floral note.";
                newPlant.growTime = 7;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Romantic;
                newPlant.value = 4;
                break;

            case 29:
                newPlant.plantName = "Cacao";
                newPlant.description = "Deep, bittersweet cacao with a rich and indulgent aroma.";
                newPlant.growTime = 7;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Romantic;
                newPlant.value = 4;
                break;


            // =========================================================
            // CREATIVE
            // =========================================================

            case 30:
                newPlant.plantName = "Butterfly Pea";
                newPlant.description = "A striking blue flower that gives every brew a little magic.";
                newPlant.growTime = 7;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Creative;
                newPlant.value = 4;
                break;

            case 31:
                newPlant.plantName = "Basil";
                newPlant.description = "Fresh and aromatic with a surprisingly playful herbal bite.";
                newPlant.growTime = 3;
                newPlant.yield = 3;
                newPlant.vibe = vibe.Creative;
                newPlant.value = 2;
                break;

            case 32:
                newPlant.plantName = "Pineapple";
                newPlant.description = "Bold tropical sweetness with a sharp and playful tang.";
                newPlant.growTime = 6;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Creative;
                newPlant.value = 3;
                break;

            case 33:
                newPlant.plantName = "Star Anise";
                newPlant.description = "A striking spice with a sweet liquorice-like flavour.";
                newPlant.growTime = 7;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Creative;
                newPlant.value = 4;
                break;


            // =========================================================
            // CONFIDENT
            // =========================================================

            case 34:
                newPlant.plantName = "Ginger";
                newPlant.description = "Hot, sharp, and unapologetically spicy.";
                newPlant.growTime = 4;
                newPlant.yield = 2;
                newPlant.vibe = vibe.Confident;
                newPlant.value = 3;
                break;

            case 35:
                newPlant.plantName = "Cardamom";
                newPlant.description = "An intensely aromatic spice with a bold, complex flavour.";
                newPlant.growTime = 7;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Confident;
                newPlant.value = 4;
                break;

            case 36:
                newPlant.plantName = "Clove";
                newPlant.description = "A small but powerful spice with a warm and assertive bite.";
                newPlant.growTime = 6;
                newPlant.yield = 1;
                newPlant.vibe = vibe.Confident;
                newPlant.value = 4;
                break;


            // =========================================================
            // REFRESHED
            // =========================================================

            case 37:
                newPlant.plantName = "Lemongrass";
                newPlant.description = "Crisp and citrusy with a wonderfully clean finish.";
                newPlant.growTime = 3;
                newPlant.yield = 3;
                newPlant.vibe = vibe.Refreshed;
                break;

            case 38:
                newPlant.plantName = "Lemon";
                newPlant.description = "Sharp, bright citrus that cuts cleanly through heavier flavours.";
                newPlant.growTime = 5;
                newPlant.yield = 3;
                newPlant.vibe = vibe.Refreshed;
                newPlant.value = 2;
                break;

            case 39:
                newPlant.plantName = "Cucumber";
                newPlant.description = "Light, crisp, and exceptionally fresh.";
                newPlant.growTime = 3;
                newPlant.yield = 3;
                newPlant.vibe = vibe.Refreshed;
                break;
        }
        return newPlant;
    }
}