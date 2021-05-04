# PCG_For_Architecture_Generation


Run the exe file in released version folder to start the generator. We only support Windows for now.

## Our grammar
- F: forward
- P: place a block
- +: turn certain degrees in Unity
- -: turn certain degrees in Unity
- []: save the current position, make a new branch


## Parameters
- Step: The distance which the generator will move in one ‘F’
- Step rotation: the degree to which the generator will rotate with one ‘+’ or ‘-’
-	Max floor: maximum height expected
-	Min floor: minimum height expected
-	Dome Number: number of domes expected
-	Bay window: if there will be a bay window
-	Multiple roofs: multiple roof styles
-	Tower: if there will be a tower-like structure
