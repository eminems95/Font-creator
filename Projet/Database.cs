using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projet
{
    public class Database
    {
        public List<CharClass> Charlist = new List<CharClass>();
        public List<CharClass> SelectedChars = new List<CharClass>();

        public void Add(string singleChar,bool[,] Char)
        {
            Charlist.Add(new CharClass() {TabCharacter = Char, character=singleChar.ToCharArray()[0]});
        }

        public void Add(List<CharClass> worklist)
        {
            foreach (CharClass element in worklist)
                Charlist.Add(element);
        }

        public StringBuilder CreateText()
        {
            StringBuilder strb = new StringBuilder();
            for(int x=0;x<5;x++)
            {
            foreach (CharClass character in SelectedChars)
            {
                for (int i = 0; i < 5; i++)
                {
                    strb.Append(character.TabCharacter[x,i]? "#":" ");
                }

                 }
            strb.AppendLine();
            }
            return strb;
            SelectedChars.Clear();
        }

        public void MakeAnArray(string text)
        {
            
            for (int i = 0; i < text.Length; i++)
            {
                foreach (CharClass character in Charlist)
                {
                    if (character.character == text[i])
                        SelectedChars.Add(character);
                }
            }
            MessageBox.Show(text);
        }

       
    }
}
