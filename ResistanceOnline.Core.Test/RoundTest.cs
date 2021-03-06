﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResistanceOnline.Core.Test
{
	[TestClass]
	public class RoundTest
	{
		[TestMethod]
		public void EasySuccess()
		{
			var players = new List<Player> { new Player(), new Player(), new Player(), new Player(), new Player() };
			var round = new Round(players, 0, 3, 1);

			//to start with we should be waiting for someone to choose the players
			Assert.AreEqual(Round.State.ProposingPlayers, round.DetermineState());

			//select the players for the quest
			round.AddToTeam(players[0], players[0]);
			Assert.AreEqual(Round.State.ProposingPlayers, round.DetermineState());
            round.AddToTeam(players[0], players[1]);
            round.AddToTeam(players[0], players[2]);

			Assert.AreEqual(Round.State.Voting, round.DetermineState());

			//do some voting to see if everyone approves
			round.VoteForTeam(players[0], true);
			Assert.AreEqual(Round.State.Voting, round.DetermineState());
			round.VoteForTeam(players[1], true);
			round.VoteForTeam(players[2], false);
			round.VoteForTeam(players[3], false);
			round.VoteForTeam(players[4], true);
			
			//do the quest
			Assert.AreEqual(Round.State.Questing, round.DetermineState());
			round.SubmitQuest(players[0], true);
			Assert.AreEqual(Round.State.Questing, round.DetermineState());
			round.SubmitQuest(players[1], true);
			round.SubmitQuest(players[2], true);

			Assert.AreEqual(Round.State.Succeeded, round.DetermineState());
		}

		[TestMethod]
		public void FailedRound()
		{
			var players = new List<Player> { new Player(), new Player(), new Player(), new Player(), new Player() };
			var round = new Round(players, 0, 3, 1);

			//to start with we should be waiting for someone to choose the players
			Assert.AreEqual(Round.State.ProposingPlayers, round.DetermineState());

			//select the players for the quest
            round.AddToTeam(players[0], players[0]);
			Assert.AreEqual(Round.State.ProposingPlayers, round.DetermineState());
            round.AddToTeam(players[0], players[1]);
            round.AddToTeam(players[0], players[2]);

			Assert.AreEqual(Round.State.Voting, round.DetermineState());

			//do some voting to see if everyone approves
			round.VoteForTeam(players[0], true);
			Assert.AreEqual(Round.State.Voting, round.DetermineState());
			round.VoteForTeam(players[1], false);
			round.VoteForTeam(players[2], false);
			round.VoteForTeam(players[3], false);
			round.VoteForTeam(players[4], true);

			//nope, try again
			Assert.AreEqual(Round.State.ProposingPlayers, round.DetermineState());
            round.AddToTeam(players[1], players[2]);
			Assert.AreEqual(Round.State.ProposingPlayers, round.DetermineState());
            round.AddToTeam(players[1], players[3]);
            round.AddToTeam(players[1], players[4]);

			Assert.AreEqual(Round.State.Voting, round.DetermineState());

			//do some voting to see if everyone approves
			round.VoteForTeam(players[0], true);
			Assert.AreEqual(Round.State.Voting, round.DetermineState());
			round.VoteForTeam(players[1], true);
			round.VoteForTeam(players[2], false);
			round.VoteForTeam(players[3], false);
			round.VoteForTeam(players[4], true);

			//do the quest
			Assert.AreEqual(Round.State.Questing, round.DetermineState());
			round.SubmitQuest(players[0], false);
			Assert.AreEqual(Round.State.Questing, round.DetermineState());
			round.SubmitQuest(players[1], false);
			round.SubmitQuest(players[2], true);

			Assert.AreEqual(Round.State.Failed, round.DetermineState());
		}

	}
}
