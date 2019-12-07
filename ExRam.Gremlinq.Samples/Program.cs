﻿// ReSharper disable ConsiderUsingConfigureAwait
using System;
using System.Threading.Tasks;
using ExRam.Gremlinq.Core;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace ExRam.Gremlinq.Samples
{
    public class Program
    {
        private Person _marko;
        private readonly IGremlinQuerySource _g;

        public Program()
        {
            _g = g
                .ConfigureEnvironment(env => env
                    //Since the Vertex and Edge classes contained in this sample implement IVertex resp. IEdge,
                    //setting a model is actually not required as long as these classes are discoverable (i.e. they reside
                    //in a currently loaded assembly). We explicitly set a model here anyway.
                    .UseModel(GraphModel.FromBaseTypes<Vertex, Edge>()))

                //Configure Gremlinq to work on a locally running instance of Gremlin server.
                .UseGremlinServer("localhost", GraphsonVersion.V3);

                //Uncomment below, comment above and enter appropriate data to configure Gremlinq to work on CosmosDB!
                //.UseCosmosDb(hostname, database, graphName, authKey);
        }

        public async Task Run()
        {
            await CreateGraph();
            await CreateKnowsRelationInOneQuery();

            await WhoDoesMarkoKnow();
            await WhoIsOlderThan30();
            await WhoseNameStartsWithB();
            await WhoKnowsWho();
            await WhatPetsAreAround();
            await WhoOwnsAPet();

            await SetAndGetMetaDataOnMarko();

            Console.Write("Press any key...");
            Console.Read();
        }

        private async Task CreateGraph()
        {
            // Uncomment to delete the whole graph on every run.
            //await _g.V().Drop().ToArrayAsync();

            _marko = await _g
                .AddV(new Person { Name = "Marko", Age = 29 })
                .FirstAsync();

            var vadas = await _g
                .AddV(new Person { Name = "Vadas", Age = 27 })
                .FirstAsync();
            
            var josh = await _g
                .AddV(new Person { Name = "Josh", Age = 32 })
                .FirstAsync();

            var peter = await _g
                .AddV(new Person { Name = "Peter", Age = 29 })
                .FirstAsync();

            var charlie = await _g
                .AddV(new Dog {Name = "Charlie", Age = 2})
                .FirstAsync();

            var luna = await _g
                .AddV(new Cat {Name = "Luna", Age = 9})
                .FirstAsync();

            var lop = await _g
                .AddV(new Software { Name = "Lop", Language = ProgrammingLanguage.Java })
                .FirstAsync();

            var ripple = await _g
                .AddV(new Software { Name = "Ripple", Language = ProgrammingLanguage.Java })
                .FirstAsync();

            await _g
                .V(_marko.Id)
                .AddE<Knows>()
                .To(__ => __
                    .V(vadas.Id))
                .FirstAsync();

            await _g
                .V(_marko.Id)
                .AddE<Knows>()
                .To(__ => __
                    .V(josh.Id))
                .FirstAsync();

            await _g
                .V(_marko.Id)
                .AddE<Created>()
                .To(__ => __
                    .V(lop.Id))
                .FirstAsync();

            await _g
                .V(josh.Id)
                .AddE<Created>()
                .To(__ => __
                    .V(ripple.Id))
                .FirstAsync();

            await _g
                .V(josh.Id)
                .AddE<Created>()
                .To(__ => __
                    .V(lop.Id))
                .FirstAsync();

            await _g
                .V(peter.Id)
                .AddE<Created>()
                .To(__ => __
                    .V(lop.Id))
                .FirstAsync();

            await _g
                .V(josh.Id)
                .AddE<Owns>()
                .To(__ => __
                    .V(charlie.Id))
                .FirstAsync();

            await _g
                .V(peter.Id)
                .AddE<Owns>()
                .To(__ => __
                    .V(luna.Id))
                .FirstAsync();
        }

        private async Task CreateKnowsRelationInOneQuery()
        {
            await _g
                .AddV(new Person { Name = "Bob", Age = 36 })
                .AddE<Knows>()
                .To(__ => __
                    .AddV(new Person { Name = "Jeff", Age = 27 }))
                .FirstAsync();
        }

        private async Task WhoDoesMarkoKnow()
        {
            var knownPersonsToMarko = await _g
                .V<Person>()
                .Where(x => x.Name.Value == "Marko")
                .Out<Knows>()
                .OfType<Person>()
                .Order(x => x.By(x => x.Name))
                .Values(x => x.Name)
                .ToArrayAsync();

            Console.WriteLine("Who does Marko know?");

            foreach (var person in knownPersonsToMarko)
            {
                Console.WriteLine($" Marko knows {person}.");
            }

            Console.WriteLine();
        }

        private async Task WhoIsOlderThan30()
        {
            var personsOlderThan30 = await _g
                .V<Person>()
                .Where(x => x.Age > 30)
                .ToArrayAsync();

            Console.WriteLine("Who is older than 30?");

            foreach (var person in personsOlderThan30)
            {
                Console.WriteLine($" {person.Name.Value} is older than 30.");
            }

            Console.WriteLine();
        }

        private async Task WhoseNameStartsWithB()
        {
            var nameStartsWithB = await _g
                .V<Person>()
                .Where(x => x.Name.Value.StartsWith("B"))
                .ToArrayAsync();

            Console.WriteLine("Whose name starts with 'B'?");

            foreach (var person in nameStartsWithB)
            {
                Console.WriteLine($" {person.Name.Value}'s name starts with a 'B'.");
            }

            Console.WriteLine();
        }

        private async Task WhoKnowsWho()
        {
            var friendTuples = await _g
                .V<Person>()
                .As((__, person) => __
                    .Out<Knows>()
                    .OfType<Person>()
                    .As((___, friend) => ___
                        .Select(person, friend)))
                .ToArrayAsync();

            Console.WriteLine("Who knows who?");

            foreach (var (person1, person2) in friendTuples)
            {
                Console.WriteLine($" {person1.Name.Value} knows {person2.Name.Value}.");
            }

            Console.WriteLine();
        }

        private async Task WhatPetsAreAround()
        {
            var pets = await _g
                .V<Pet>()
                .ToArrayAsync();

            Console.WriteLine("What pets are there?");

            foreach (var pet in pets)
            {
                Console.WriteLine($" There is {pet.Name.Value}.");
            }

            Console.WriteLine();
        }

        private async Task WhoOwnsAPet()
        {
            var petOwners = await _g
                .V<Person>()
                .Where(__ => __
                    .Out<Owns>()
                    .OfType<Pet>())
                .ToArrayAsync();

            //Alternatively:
            //var petOwners = await _g
            //    .V<Pet>()
            //    .In<Owns>()
            //    .OfType<Person>()
            //    .Dedup()
            //    .ToArray();

            Console.WriteLine("Who owns a pet?");

            foreach (var petOwner in petOwners)
            {
                Console.WriteLine($" {petOwner.Name.Value} owns a pet.");
            }

            Console.WriteLine();
        }

        private async Task SetAndGetMetaDataOnMarko()
        {
            await _g
                .V<Person>(_marko.Id)
                .Properties(x => x.Name)
                .Property(x => x.Creator, "Stephen")
                .Property(x => x.Date, DateTimeOffset.Now)
                .ToArrayAsync();

            var metaProperties = await _g
                .V()
                .Properties()
                .Properties()
                .ToArrayAsync();

            Console.WriteLine("Meta properties on Vertex properties:");

            foreach (var metaProperty in metaProperties)
            {
                Console.WriteLine($" {metaProperty}");
            }

            Console.WriteLine();
        }

        static async Task Main()
        {
            await new Program().Run();
        }
    }
}
