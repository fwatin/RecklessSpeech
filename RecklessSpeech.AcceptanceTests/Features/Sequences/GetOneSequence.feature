Feature: Get one sequence

    Scenario: Happy Path - Retrieve one sequence
        Given an existing sequence
        When the user retrieves this sequence
        Then the existing sequence is returned

    @ErrorHandling
    Scenario: Get an unknown sequence
        When the user tries to get an unknown sequence
        Then an error is thrown with an HTTP status 404 and error type "Read_Sequence_NotFound"