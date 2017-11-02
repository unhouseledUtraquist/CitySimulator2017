﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus.Extensions.Canada;
using Bogus;
using ConsoleDump;
using ServerForTheLogic.Utilities;
using ServerForTheLogic.Infrastructure;
using Newtonsoft.Json;
using ServerForTheLogic.Json;

namespace ServerForTheLogic
{
    /// <summary>
    /// Driver for the City Simulator program
    /// </summary>
    class Program
    {
        public static List<Person> People = new List<Person>();
        private const int MEAN_DEATH_AGE = 80;
        private const int STANDARD_DEVIATION_DEATH = 14;
        private static City city;
        private static Creator creator;

        /// <summary>
        /// Entry point for the city simulator program
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ////Person me = new Person();
            ////me.Dump();
            //Person me = new Person();
            //for (int i = 0; i < 24; i++) {
            //    me = new Person();
            //   // me.createPerson();
            //    People.Add(me);
            //    me = null;
            //}
            //People.Dump();
            //me = new Person();
            //// me.KeepOpen();
            //KeepOpen();
            DatabaseLoader loader = new DatabaseLoader();
            city = loader.loadCity();
            Block b, b1, b2;
            if (city == null)
            {
                //TEST DATA 
                city = new City();
                creator = new Creator();
                //fill 3 blocks
                //b = creator.addRoadsToEmptyBlock(new Block(new Point(51, 98)), city);
                //b1 = creator.addRoadsToEmptyBlock(new Block(new Point(51, 91)), city);
                //b2 = creator.addRoadsToEmptyBlock(new Block(new Point(54, 91)), city);
                Console.WriteLine("BlockMap Length = " + city.BlockMap.GetLength(0));
                Console.WriteLine("BlockMap Width = " + city.BlockMap.GetLength(1));

                creator.addRoadsToEmptyBlock(city.BlockMap[city.BlockMap.GetLength(0)/2, city.BlockMap.GetLength(1) / 2],city);
                //stick some buildings in them blocks
                //creator.createBuilding(city, b);
                //creator.createBuilding(city, b1);
                //creator.createBuilding(city, b2);
            }
            //sets the adjacent blocks of the specified block
            foreach (Block block in city.BlockMap)
            {
                city.setAdjacents(block);
            }

             

            Console.WriteLine("expand");
            for (int i = 0; i < city.BlockMap.GetLength(0); i++)
            {
                city.expandCity();
            }

            //Console.WriteLine(b.Adjacents.Count);
            //Console.WriteLine(b1.Adjacents.Count);
            //Console.WriteLine(b2.Adjacents.Count);
            foreach (Block block in city.BlockMap)
                if (block.Adjacents.Count > 8)
                    Console.WriteLine("Too many:" + block.ToString());
            //city.printBlockMapTypes();
            printCity();
            Updater<City> updater = new Updater<City>();
            updater.sendFullUpdate(city, Formatting.Indented);
            KeepOpen();
        }
        /// <summary>
        /// Keeps the program running 
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public static void KeepOpen()
        {
            while (true) ;
        }

        /// <summary>
        /// Prints a block represented as symbols/letters in a neatly formatted manner
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        /// <param name="b"></param>
        public static void printBlock(Block b)
        {
            for (int i = 0; i < Block.BLOCK_WIDTH; i++)
            {
                for (int j = 0; j < Block.BLOCK_LENGTH; j++)
                {
                    if (b.LandPlot[i, j] != null)
                    {
                        Console.Write(b.LandPlot[i, j].Type);
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints the city represented as symbols/letters in a neatly formatted manner
        /// <para/> Last editted:  2017-10-02
        /// </summary>
        public static void printCity()
        {
            for (int i = 0; i < City.CITY_WIDTH; ++i)
            {
                for (int j = 0; j < City.CITY_LENGTH; ++j)
                {
                    if (city.Map[i, j] != null)
                    {
                        Console.Write(city.Map[i, j].Type);
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }


    }
}
