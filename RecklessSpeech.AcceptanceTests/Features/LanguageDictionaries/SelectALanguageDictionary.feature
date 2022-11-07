Feature: Select A Language Dictionary

Scenario: Select an existing language dictionary
	Given an existing language dictionary
	And an existing sequence
	When the user selects a dictionary for a sequence
	Then the dictionary is saved for this sequence