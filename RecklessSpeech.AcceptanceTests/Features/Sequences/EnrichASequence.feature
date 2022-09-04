Feature: Enrich A Sequence

Scenario: Happy Path - the dutch sequence is enriched 
	Given a sequence to be enriched
	When the user enriches this sequence
	Then the sequence enriched data contains the raw explanation