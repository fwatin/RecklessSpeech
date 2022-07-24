Feature: Import new sequences

Scenario: Happy Path - import some new sequences
	Given a file containing some sequences
	When the users imports this file
	Then some sequences are saved