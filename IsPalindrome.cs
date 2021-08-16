string word = "essfsze";
int halfLength = (word.Length-1) / 2;
for(int index = 0; index <= halfLength; index++)
{
    if(word[index] != word[word.Length - 1 - index]){
        Console.WriteLine(false);
        return;
    }
}
Console.WriteLine(true);