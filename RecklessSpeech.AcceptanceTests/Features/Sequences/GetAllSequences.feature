Feature: Get all sequences

	Scenario: Retrieve some sequences
		Given an existing sequence
		When the user retrieves sequences
		Then the existing sequence is returned