# username-generator

Username Generator

This application allows you to create a random username from a text file.

The username consists of 3 parts:
- 1 random word (from the French dictionary)
- 1 random word (from the list of positive words) 
- 1 random number (between 0 and 1000)

The text file must contain one element per line with a line break.
example :
wordA
wordB
wordC

You can also find the example file (liste-français.txt) in this project.

For the first use, don't forget to uncomment line 28 of the Index.razor and replace "your full file path" with your own path.
This method uses MongoDB database.
Comment out this line again from the second use to avoid creating a database each time.

You can also customize the ID ranges in lines 77 and 79 of the PseudoGenerator.cs to suit your text files structure.
- first part of word has ID 0 - 22741
- second part of word has ID 22742 - 22851

Have fun with this generator!
