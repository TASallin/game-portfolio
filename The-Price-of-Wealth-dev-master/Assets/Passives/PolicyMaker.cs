public class PolicyMaker : Passive {
	
	public PolicyMaker (Character c) {self = c; name = "PolicyMaker"; description = "Sets a policy for the round if not in the lead";}
	
	public override TimedMethod[] Check(bool player) {
		System.Random rng = new System.Random();
		int num = rng.Next(4);
		string message;
		if (num == 0) {
			foreach(Character c in Party.members) {
				if (c != null && c.GetAlive()) {
	    			Status.NullifyDefense(c);
		    		c.GainCharge(3);
				}
		 	}
			foreach(Character c in Party.enemies) {
				if (c != null && c.GetAlive()) {
			    	Status.NullifyDefense(c);
			    	c.GainCharge(3);
		    	}
			}
			message = self.ToString() + " mandated a policy of aggression";
			return new TimedMethod[] {new TimedMethod(0, "CharLogSprite", new object[] {"def reset", 0, "nullDefense", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"def reset", 1, "nullDefense", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"def reset", 2, "nullDefense", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"def reset", 3, "nullDefense", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"def reset", 0, "nullDefense", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"def reset", 1, "nullDefense", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"def reset", 2, "nullDefense", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"def reset", 3, "nullDefense", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 0, "charge", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 1, "charge", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 2, "charge", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 3, "charge", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 0, "charge", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 1, "charge", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 2, "charge", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 3, "charge", false}),
				new TimedMethod(60, "Log", new object[] {message})};
		} else if (num == 1) {
			foreach(Character c in Party.members) {
				if (c != null && c.GetAlive()) {
	    			Status.NullifyAttack(c);
		    		c.GainGuard(3);
				}
		 	}
			foreach(Character c in Party.enemies) {
				if (c != null && c.GetAlive()) {
			    	Status.NullifyAttack(c);
			    	c.GainGuard(3);
		    	}
			}
			message = self.ToString() + " mandated a policy of safety";
			return new TimedMethod[] {new TimedMethod(0, "CharLogSprite", new object[] {"atk reset", 0, "nullAttack", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"atk reset", 1, "nullAttack", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"atk reset", 2, "nullAtttack", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"atk reset", 3, "nullAttack", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"atk reset", 0, "nullAttack", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"atk reset", 1, "nullAttack", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"atk reset", 2, "nullAttack", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"atk reset", 3, "nullAttack", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 0, "guard", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 1, "guard", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 2, "guard", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 3, "guard", true}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 0, "guard", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 1, "guard", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 2, "guard", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"3", 3, "guard", false}),
				new TimedMethod(60, "Log", new object[] {message})};
		} else if (num == 2) {
			foreach(Character c in Party.enemies) {
				if (c != null && c.GetAlive()) {
			        c.GainPower(1);
			    	c.GainDefense(1);
		    	}
			}
			message = self.ToString() + " mandated a corrupt policy to benefit their supporters";
			return new TimedMethod[] {new TimedMethod(0, "CharLogSprite", new object[] {"1", 0, "power", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"1", 1, "power", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"1", 2, "power", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"1", 3, "power", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"1", 0, "defense", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"1", 1, "defense", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"1", 2, "defense", false}),
				new TimedMethod(0, "CharLogSprite", new object[] {"1", 3, "defense", false}),
				new TimedMethod(60, "Log", new object[] {message})};
		} else {
			int amount = 0;
			foreach(Character c in Party.enemies) {
				if (c != null && c.GetAlive() && c != self) {
			        c.SetHealth(System.Math.Max(1, c.GetHealth() - 5));
					amount += 2;
		    	}
			}
			self.Heal(amount);
			message = self.ToString() + " mandated a policy of sacrifice to your president";
			return new TimedMethod[] {new TimedMethod(0, "CharLogSprite", new object[] {amount.ToString(), 3, "healing", false}),
				new TimedMethod(60, "Log", new object[] {message})};
		}
	}
}