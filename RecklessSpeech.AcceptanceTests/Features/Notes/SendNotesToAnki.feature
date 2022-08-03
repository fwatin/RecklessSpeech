Feature: Send Notes to Anki

    Scenario: Send some note to Anki
        Given an existing sequence
        When the user sends the sequence to Anki
        Then a corresponding note is sent to Anki