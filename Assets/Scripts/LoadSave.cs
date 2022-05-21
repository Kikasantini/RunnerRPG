using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadSave : MonoBehaviour
{
    public CharacterSO character;
    public IntVariable level;
    public IntVariable exp;
    public IntVariable coins;
    public IntVariable gems;
    public IntVariable tokenChest;
    public IntVariable tokenCape;
    public IntVariable tokenPants;
    public IntVariable tokenShoes;
    public IntVariable tokenWeapon;
    public EquipmentSO chest;
    public EquipmentSO cape;
    public EquipmentSO pants;
    public EquipmentSO shoes;
    public EquipmentSO weapon;
    public BoolVariable brokenMachine;
    public IntVariable battleID;
    public BossSO bossA;
    public BossSO bossB;

    private string currentPath;


    void Start()
    {
        //Get the path of the Game data folder
        currentPath = Application.persistentDataPath;

        //Output the Game data path to the console
        Debug.Log("dataPath : " + currentPath);
        currentPath += "\\save.file";

        if (!File.Exists(currentPath))
        {
            Debug.Log("File created");
            FileStream fs = File.Create(currentPath);
            fs.Close();
            WriteFirstSave();
        }

        LoadValues();
    }
    public void LoadValues()
    {
        string line;
        string[] lineElements;

        StreamReader reader = new StreamReader(currentPath);

        try
        {
            line = reader.ReadLine();
            lineElements = line.Split();
            level.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            exp.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            character.stars = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            character.skill[0].quantity = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            character.skill[1].quantity = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            character.skill[2].quantity = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            coins.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            gems.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            tokenChest.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            tokenCape.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            tokenPants.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            tokenShoes.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            tokenWeapon.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            chest.level = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            cape.level = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            pants.level = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            shoes.level = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            weapon.level = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            if (lineElements[0] == "True")
                brokenMachine.Value = true;
            else
                brokenMachine.Value = false;

            line = reader.ReadLine();
            lineElements = line.Split();
            battleID.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            bossA.level = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            bossB.level = Int32.Parse(lineElements[0]);

            //Debug.Log("leu tudo");            
        }
        catch (Exception)
        {
            Debug.Log("Deu ruim no read");
        }

        //close the file
        reader.Close();
    }

    public void SaveValues()
    {
        StreamWriter writer = new StreamWriter(currentPath);
        try
        {
            //Write a line of text
            writer.WriteLine(level.Value + " level");
            writer.WriteLine(exp.Value + " exp");
            writer.WriteLine(character.stars + " stars");
            writer.WriteLine(character.skill[0].quantity + " skill 1");
            writer.WriteLine(character.skill[1].quantity + " skill 2");
            writer.WriteLine(character.skill[2].quantity + " skill 3");
            writer.WriteLine(coins.Value + " coins");
            writer.WriteLine(gems.Value + " gems");
            writer.WriteLine(tokenChest.Value + " token chest");
            writer.WriteLine(tokenCape.Value + " token cape");
            writer.WriteLine(tokenPants.Value + " token pants");
            writer.WriteLine(tokenShoes.Value + " token shoes");
            writer.WriteLine(tokenWeapon.Value + " token weapon");
            writer.WriteLine(chest.level + " level chest");
            writer.WriteLine(cape.level + " level cape");
            writer.WriteLine(pants.level + " level pants");
            writer.WriteLine(shoes.level + " level shoes");
            writer.WriteLine(weapon.level + " level weapon");
            writer.WriteLine(brokenMachine.Value.ToString() + " brokenMachine status");
            writer.WriteLine(battleID.Value.ToString() + " battle ID");
            writer.WriteLine(bossA.level + " boss A level");
            writer.WriteLine(bossB.level + " boss B level");

            //Debug.Log("escreveu tudo");
        }
        catch (Exception)
        {
            Console.WriteLine("Deu ruim no save");
        }
        //Close the file
        writer.Close();
    }

    private void WriteFirstSave()
    {
        StreamWriter writer = new StreamWriter(currentPath);
        try
        {
            writer.WriteLine("0 level");
            writer.WriteLine("0 exp");
            writer.WriteLine("0 stars");
            writer.WriteLine("0 skill 1");
            writer.WriteLine("0 skill 2");
            writer.WriteLine("0 skill 3");
            writer.WriteLine("0 coins");
            writer.WriteLine("0 gems");
            writer.WriteLine("0 token chest");
            writer.WriteLine("0 token cape");
            writer.WriteLine("0 token pants");
            writer.WriteLine("0 token shoes");
            writer.WriteLine("0 token weapon");
            writer.WriteLine("0 level chest");
            writer.WriteLine("0 level cape");
            writer.WriteLine("0 level pants");
            writer.WriteLine("0 level shoes");
            writer.WriteLine("0 level weapon");
            writer.WriteLine("False brokenMachine status");
            writer.WriteLine("0 battle ID");
            writer.WriteLine("0 boss A level");
            writer.WriteLine("0 boss B level");

            //Debug.Log("escreveu tudo");
        }
        catch (Exception)
        {
            Console.WriteLine("Deu ruim no save - first file");
        }
        //Close the file
        writer.Close();
    }

    /*
    private void ImOnPC()
    {
        currentPath = Directory.GetCurrentDirectory();
        currentPath += "\\save.file";
        //Debug.Log("current path = " + currentPath);

        if (!File.Exists(currentPath))
        {
            FileStream fs = File.Create(currentPath);
            fs.Close();
            WriteFirstSaveFile_PC(currentPath);
        }

        LoadValues();
    }
    

    private void WriteFirstSaveFile_PC(string currentPath)
    {
        StreamWriter writer = new StreamWriter(currentPath);
        try
        {
            writer.WriteLine("0 level");
            writer.WriteLine("0 exp");
            writer.WriteLine("0 stars");
            writer.WriteLine("0 skill 1");
            writer.WriteLine("0 skill 2");
            writer.WriteLine("0 skill 3");
            writer.WriteLine("0 coins");
            writer.WriteLine("0 gems");
            writer.WriteLine("0 token chest");
            writer.WriteLine("0 token cape");
            writer.WriteLine("0 token pants");
            writer.WriteLine("0 token shoes");
            writer.WriteLine("0 token weapon");
            writer.WriteLine("0 level chest");
            writer.WriteLine("0 level cape");
            writer.WriteLine("0 level pants");
            writer.WriteLine("0 level shoes");
            writer.WriteLine("0 level weapon");
            writer.WriteLine("False brokenMachine status");

            //Debug.Log("escreveu tudo");
        }
        catch (Exception)
        {
            Console.WriteLine("Deu ruim no save - first file");
        }
        //Close the file
        writer.Close();
    }


    private void ImOnAndroid()
    {
        // Teste:
        
        //Get the path of the Game data folder
        currentPath = Application.persistentDataPath;

        //Output the Game data path to the console
        Debug.Log("dataPath : " + currentPath);

        currentPath += "\\save.file";
        //testeText.text = currentPath;
        Debug.Log("current path = " + currentPath);

        if (!File.Exists(currentPath))
        {
            //testeText.text = "FILE já existia : " + currentPath;
            Debug.Log("criou arquivo");
            FileStream fs = File.Create(currentPath);
            fs.Close();
            WriteFirstSaveFile_Android();
        }
        LoadValues();
    }
    

    private void LoadPCBuild()
    {
        string line;
        string[] lineElements;

        StreamReader reader = new StreamReader(currentPath);

        try
        {
            line = reader.ReadLine();
            lineElements = line.Split();
            level.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            exp.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            character.stars = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            character.skill[0].quantity = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            character.skill[1].quantity = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            character.skill[2].quantity = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            coins.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            gems.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            tokenChest.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            tokenCape.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            tokenPants.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            tokenShoes.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            tokenWeapon.Value = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            chest.level = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            cape.level = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            pants.level = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            shoes.level = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            weapon.level = Int32.Parse(lineElements[0]);

            line = reader.ReadLine();
            lineElements = line.Split();
            if (lineElements[0] == "True")
                brokenMachine.Value = true;
            else
                brokenMachine.Value = false;

            //Debug.Log("leu tudo");
        }
        catch (Exception)
        {
            Debug.Log("Deu ruim no read");
        }

        //close the file
        reader.Close();
    }

    private void SavePCBuild()
    {
        StreamWriter writer = new StreamWriter(currentPath);
        try
        {
            //Write a line of text
            writer.WriteLine(level.Value + " level");
            writer.WriteLine(exp.Value + " exp");
            writer.WriteLine(character.stars + " stars");
            writer.WriteLine(character.skill[0].quantity + " skill 1");
            writer.WriteLine(character.skill[1].quantity + " skill 2");
            writer.WriteLine(character.skill[2].quantity + " skill 3");
            writer.WriteLine(coins.Value + " coins");
            writer.WriteLine(gems.Value + " gems");
            writer.WriteLine(tokenChest.Value + " token chest");
            writer.WriteLine(tokenCape.Value + " token cape");
            writer.WriteLine(tokenPants.Value + " token pants");
            writer.WriteLine(tokenShoes.Value + " token shoes");
            writer.WriteLine(tokenWeapon.Value + " token weapon");
            writer.WriteLine(chest.level + " level chest");
            writer.WriteLine(cape.level + " level cape");
            writer.WriteLine(pants.level + " level pants");
            writer.WriteLine(shoes.level + " level shoes");
            writer.WriteLine(weapon.level + " level weapon");
            writer.WriteLine(brokenMachine.Value.ToString() + " brokenMachine status");

            //Debug.Log("escreveu tudo");
        }
        catch (Exception)
        {
            Console.WriteLine("Deu ruim no save");
        }
        //Close the file
        writer.Close();
    }
    */
}
