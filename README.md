# VisualHostWpfApp

### 1) First let's see how it's working now

when the app is run -> the busy insicator is moving 
if you press the button
- the busy indicator keeps on moving
- the app does a long work unit while busy indicator is moving

### 2) the desired behaviour is

when the app is run -> the busy indicator is invizible
if you press the button 
- the busy indicator becomes vizible
- meanwhile the app does the work unit
- when the app finishes the work unit -> the busy indicator becomes invisible 

### 3) the important things are:
the aim is to master the approach described in the article:
https://blogs.msdn.microsoft.com/dwayneneed/2007/04/26/multithreaded-ui-hostvisual/

this means that 
- no BackgroundWorker
- no Task-s

The purpose is to create a busy indicator for a single threaded app
the approach described in the article should be used