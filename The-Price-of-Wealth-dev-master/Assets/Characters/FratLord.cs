public class FratLord : Character {
	
	int test;
	int phase;
	
	public FratLord() {
	    health = 80; maxHP = 80; strength = 4; power = 0; charge = 0; defense = 2; guard = 0;
		baseAccuracy = 12; accuracy = 12; dexterity = 2; evasion = 0; type = "Fraternity Lord"; passive = new Car(this);
		quirk = Quirk.GetQuirk(this); special = null; player = false; champion = true; recruitable = false;
        CreateDrops(); test = 0; phase = 0; status.regeneration = 5;
    }
	
	public override TimedMethod[] AI () {
		if (phase == 0) {
			if (test == 0) {
				return Pledge();
			} else if (test == 1) {
				return First();
			} else if (test == 2) {
				return Second();
			} else if (test == 3) {
				return Third();
			} else {
				return Pass();
			}
		} else {
			System.Random rng = new System.Random();
			int num = rng.Next(10);
			if (num < 4) {
				return Attack();
			} else if (num < 7) {
				return Drink();
			} else {
				return Fireworks();
			}
		}
	}
	
	public TimedMethod[] Pledge() {
		TimedMethod[] goopPart;
    	if (Party.playerCount > 1) {
	    	int steps = new System.Random().Next(Party.playerCount);
		    for (int i = 0; i < 4; i++) {
			    if (Party.members[i] != null && Party.members[i].GetAlive()) {
				   	if (steps == 0) {
						test++;
						goopPart = Party.members[i].status.Goop();
					    return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " selected a pledge"}),
							new TimedMethod(0, "Audio", new object[] {"Whistle"}),
					    	new TimedMethod(0, "SwitchTo", new object[] {i + 1}), goopPart[0], goopPart[1]};
    				} else {
	    				steps--;
		    		}
		    	}
    		}
     	}
	    test++;
		goopPart = Party.members[Party.playerSlot - 1].status.Goop();
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " selected a pledge"}),
			new TimedMethod(0, "Audio", new object[] {"Whistle"}), goopPart[0], goopPart[1]};
	}
	
	public TimedMethod[] First () {
		test++;
		Party.GetPlayer().status.Goop();
		Party.AddEnemy(new CanTower());
		Party.enemies[1].GainEvasion(new System.Random().Next(3) + 12);
		Party.enemySlot = 2;
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " gave the first test: hit the cans"}),
		    new TimedMethod(60, "EnemySwitch", new object[] {1, 2})};
	}
	
	public TimedMethod[] Second () {
		test++;
		Party.GetPlayer().status.gooped = false;
		Party.AddEnemy(new Bonfire());
		Party.enemies[1].GainEvasion(99);
		Party.enemySlot = 2;
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " gave the second test: jump the fire"}),
		    new TimedMethod(60, "EnemySwitch", new object[] {1, 2})};
	}
	
	public TimedMethod[] Third () {
		test++;
		Party.GetPlayer().status.Goop();
		Party.enemies[1] = null;
		Party.AddEnemy(new Watermelon());
		Party.enemies[1].GainDefense(new System.Random().Next(4));
		Party.enemySlot = 2;
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " gave the third test: destroy the melon"})};
	}
	
	public TimedMethod[] Attack() {
		Attacks.SetAudio("Metal Hit", 6);
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " swung metal greek letters"}),
			new TimedMethod(0, "Audio", new object[] {"Small Swing"}),
		    new TimedMethod(0, "Attack", new object[] {false})};
	}
	
	public TimedMethod[] Drink() {
		int amount = new System.Random().Next(5) + 10;
		Heal(amount);
		GainPower(2);
		GainDefense(-2);
		GainAccuracy(-1);
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " drank from the red solo cup"}),
			new TimedMethod(0, "Audio", new object[] {"Drink"}),
			new TimedMethod(0, "CharLogSprite", new object[] {amount.ToString(), Party.enemySlot - 1, "healing", false}),
			new TimedMethod(0, "CharLogSprite", new object[] {"2", Party.enemySlot - 1, "power", false}),
			new TimedMethod(0, "CharLogSprite", new object[] {"-2", Party.enemySlot - 1, "defense", false}),
			new TimedMethod(0, "CharLogSprite", new object[] {"-1", Party.enemySlot - 1, "accuracy", false})};
	}
	
	public TimedMethod[] Fireworks() {
		Attacks.SetAudio("Fire Hit", 8);
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " improperly set off fireworks"}),
			new TimedMethod(0, "Audio", new object[] {"S Explosion"}),
		    new TimedMethod(0, "AttackAll", new object[] {false, 3, 3, GetAccuracy() / 2, true})};
	}
	
	public TimedMethod[] Fail() {
		test = 0;
		Attacks.SetAudio("L Explosion", 20);
		Party.GetPlayer().status.gooped = false;
		Party.enemySlot = 1;
		Party.enemies[1].Damage(999);
		Party.enemies[1].SetAlive(false);
		TimedMethod[] apathyPart;
		if (Attacks.EvasionCheck(Party.GetPlayer(), GetAccuracy() * 2)) {
		    apathyPart = Party.GetPlayer().status.CauseApathy(3);
		} else {
			apathyPart = new TimedMethod[] {new TimedMethod("Null")};
		}
		return new TimedMethod[] {new TimedMethod(0, "EnemySwitch", new object[] {1, 2}),
    		new TimedMethod(60, "Log", new object[] {ToString() + " gave the price for failure"}),
			new TimedMethod(0, "Audio", new object[] {"Filibuster"}),
		    new TimedMethod(0, "StagnantAttack", new object[] {false, 10, 18, GetAccuracy() * 2, true, true, false}), apathyPart[0]};
	}
	
	public TimedMethod[] Pass() {
		Party.GetPlayer().status.gooped = false;
		Party.GetPlayer().GainPower(5);
		Party.GetPlayer().Heal(10);
		status.regeneration = 0;
		phase = 1;
		return new TimedMethod[] {new TimedMethod(0, "Audio", new object[] {"Finale"}),
			new TimedMethod(60, "Log", new object[] {Party.GetPlayer().ToString() + " passed the test!"}),
			new TimedMethod(0, "CharLogSprite", new object[] {"Cleaned", Party.enemySlot - 1, "goop", true}),
			new TimedMethod(0, "CharLogSprite", new object[] {"5", Party.enemySlot - 1, "power", true}),
			new TimedMethod(0, "CharLogSprite", new object[] {"10", Party.enemySlot - 1, "healing", true})};
	}
	
	public override void CreateDrops() {
		drops = ItemDrops.GuaranteedDrop(new Item[] {new Smartphone(), new ExplosiveBrew(), new Pizza(), new Sword(), new Football()},
		    ItemDrops.Amount(2, 3), new StrengthPotion());	
	}
	
	public override Item[] Loot () {
		System.Random rnd = new System.Random();
		int sp = 6 + rnd.Next(5);
		Party.UseSP(sp * -1);
		Item[] dropped = drops;
		Areas.defeatedF = true;
		drops = new Item[0];
		return dropped;
	}
	
	public override string[] CSDescription () {
		return new string[] {"Fraternity Lord - the leader of the most popular fraternity here",
		    "Which one is that? ...",
		    "Anyway, try to pass the pledge tests or bad things happen"};
	}
}