module ProposeAbstractTest

open System
open NUnit.Framework

open Model
open Commands
open Events
open Testbase

// Scenario
let heimeshoff = { Firstname = "Marco";  Lastname = "Heimeshoff"; Id = OrganizerId <| Guid.NewGuid() }
let talk = proposedTalk()


[<Test>]
let ``Can propose an abstract when Call for Papers is open`` () =
  Given [ 
    OrganizerAddedToConference heimeshoff 
    CallForPapersOpened]
  |> When (ProposeAbstract talk)
  |> ThenExpect [AbstractWasProposed talk]


[<Test>]
let ``Can not propose an abstract when Call for Papers is not opened yet`` () =
  Given [ 
    OrganizerAddedToConference heimeshoff]
  |> When (ProposeAbstract talk)
  |> ThenExpect [ProposingDenied "Call For Papers Not Opened"]


[<Test>]
let ``Can not propose an abstract when Call for Papers is already closed`` () =
  Given [ 
    OrganizerAddedToConference heimeshoff 
    CallForPapersClosed]
  |> When (ProposeAbstract talk)
  |> ThenExpect [ProposingDenied "Call For Papers Closed"]
