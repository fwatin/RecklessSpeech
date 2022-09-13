Feature: Get one sequence

	Scenario: Happy Path - Retrieve one sequence
		Given an existing sequence
		When the user retrieves this sequence
		Then the existing sequence is returned