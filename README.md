# MakeIt.Random

MakeIt.Random is a fast, flexible, extensible, and feature rich random number
generation library for Unity. It provides classes and functions, accessible from
scripts, for generating random data of various sorts, including:

* bits
* integers
* floating point numbers
* angles
* vectors and quaternions
* strings
* dice rolls
* colors

In addition to simply generating random data, it also includes utilities for the
following (with support for non-uniform weights, where applicable):

* evaluating probabilities
* sampling from distributions
* selecting elements from a list
* selecting values from an enumeration
* selecting values from the set {-1, 0, +1}
* shuffling a listOne Paragraph of project description goes here

## Getting Started

### Prerequisites

MakeIt.Random depends on the following Unity packags:

* [MakeIt.Core](https://github.com/againey)
* [Unity Test Framework](https://docs.unity3d.com/Packages/com.unity.test-framework@latest/)

Optional dependencies will unlock additional features:

* [MakeIt.Colorful](https://github.com/againey) for random color generation
* [NSubstitute](https://github.com/nsubstitute/NSubstitute) for additional testing

### Installing

This repository is arranged as a valid Unity package that can be imported into a
Unity project using the instructions in [the Unity Package Manager Manual](https://docs.unity3d.com/Manual/upm-ui-giturl.html).

### How MakeIt.Random Is Organized

The foundation of MakeIt.Random is the concept of a Random Engine, an object
providing the basic services of a pseudo-random number generator (PRGN),
responsible for generating a deterministic sequence of seemingly random bits
initialized by a seed value. Unlike `UnityEngine.Random`, MakeIt.Random engines
are standard non-static classes which can be passed around, copied, and
serialized. Multiple instances can exist simultaneously and independently,
making it easier to control deterministic behavior when it is needed. (A static
global instance does exist for convenience, however.)

On top of random engines, a wide variety of extension methods are available for
using the random bits they generate to produce all of the more structured and
complex data typically required by games. Because they are extension methods
which apply to the `MakeIt.Random.IRandom` interface which all MakeIt.Random
engines implement, it is easy to add further extension methods that will
immediately become usable by any random engine type. Similarly, if for some
reason none of the random engines supplied by MakeIt.Random are suitable and
you need to create your own, just make sure it implements IRandom and your new
engine will automatically support all of the already existing utilities.

### Using MakeIt.Random

The first step in using MakeIt.Random is to pull in the namespace
`MakeIt.Random` near the top of any script file in which you plan to use it.
Without doing so, none of the extension functions for `IRandom` will not be
available, making usage syntax far more verbose.
```
using Experilous.MakeItRandom;
```

The next step is to create an instance of a random engine, probably within an
`Awake()` or `Start()` function. The easiest way to do that is by using
`MIRandom.CreateStandard()`. This selects a random engine type that is suitable
for general use and constructs an instance of that type. If you pass no
parameters, the engine will be set to a reasonably unpredictable state. You may
instead pass a seed, such as an int or a string, to generate the same sequence
each time the program is executed.
```
// Create a random engine with an unpredictable state.
var random = MIRandom.CreateStandard();
```

If you know which random engine type you wish to use (`XorShift128Plus` is a
good choice for most circumstances), then you may use a `Create()` function on
that type to do so, just as with `MIRandom.CreateStandard()`.
```
// Create a random engine, initializing its state with a string seed.
var random = XorShift128Plus.Create("My random seed!!1!11!~!");
```

Using that created instance, you can now call any of the numerous extension
methods, depending on your needs. Here are some examples:
```
// Generate an integer where 1 <= n <= 10
int num1to10 = random.RangeCC(1, 10);

// Generate a number where 0 <= n < 1
float num0to1 = random.FloatCO();

// Flip a coin
bool heads = random.Chance();

// Check a 3 in 10 probability
bool criticalHit = random.Probability(3, 10);

// Generate a random height for a male character in cm
float height = random.NormalSample(176f, 8f);

// Generate an angle in degrees where -90 < n < +90
float angleNeg90toPos90 = random.SignedHalfAngleDegOO();

// Generate +1, -1, or 0 where +1 is twice as likely as -1, and 0 is rare
int numPNZ = random.SignOrZero(200, 100, 1);

// Roll 43 20-sided dice
int[] diceRolls = random.RollDice(43, 20);

// Roll 5 4-sided dice and just keep the sum
int diceRollSum = random.SumRollDice(5, 4);

// Shuffle the array of earlier dice rolls
random.Shuffle(diceRolls);

// Select a random die roll from the array
int dieRoll = random.Element(diceRolls);

// Select index where higher die rolls are more likely to be picked
int dieRollIndex = random.WeightedIndex(diceRolls);

// Generate a random 3D unit vector
Vector3 direction = random.UnitVector3();

// Generate a position within a cirle of radius = 5
Vector2 point = random.PointWithinCircle(5f);

// Generate a 32-character string with only alpha-numeric characters.
string str = random.AlphaNumericString(32);

// Make a generator which will produce random font styles.
var fontStyleGen = random.MakeEnumGenerator<UnityEngine.FontStyles>();
// Generate a font style
var style = fontStyleGen.Next();

// Pick a random warm color
var color = random.ColorWarm();
// Alter the color's hue randomly by no more than 1/10.
color = random.HueShift((ColorHSV)color, 0.1f);
```

## Running the Tests

MakeIt.Random includes tests that can be executed by the [Unity Test Runner](https://docs.unity3d.com/2019.1/Documentation/Manual/testing-editortestsrunner.html).

### Property-based Tests

Because of the nature of PRNGs, some of the tests are not quick unit tests, but
are slower property-based tests. They generate a large quantity of pseudo-random
values and validate that every single value has the expected properties, such as
being within the specified range, or being a unit vector. There are also
statistical properties, such as having the expected standard deviation.

Because these tests are so much slower than typical unit tests, they all include
a variant with the category `Smoke` which only tests samples that are 1% of the
full sample size. To exclude the long tests and just run the smoke tests, select
the `Smoke` category and deselect the `Statistical` category in the Unity Test
Runner before running tests.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/your/project/tags). 

MakeIt.Random was originally part of a collection of private projects in a
single repository, so some of that shared history still exists within this
repository. Releases for other projects before the split are also tagged, such
as `makeit-colorful-v1.0`.

## Authors

* **Andy Gainey**

## License

This project is licensed under the Apache License, Version 2.0 - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Random engine state initialization uses the [FNV-1a](http://www.isthe.com/chongo/tech/comp/fnv/) hash function, developed by Glenn Fowler, Phong Vo, and Landon Curt Noll.
* The foundational work for multiple random engines is [Xorshift RNGs](https://www.jstatsoft.org/article/view/v008i14) by George Marsaglia.
* `XorShift128Plus` is an implementation of the XorShift modifications by Sebastiano Vigna in his paper [*Further scramblings of Marsaglia's xorshift generators*](http://vigna.di.unimi.it/ftp/papers/xorshiftplus.pdf).
* `XorShift1024Star` is an implementation of the XorShift modifications by Sebastiano Vigna, adapted from a [C code reference implementation](http://xoroshiro.di.unimi.it/xorshift1024star.c).
* `XorShiftAdd` is an implementation of the XorShift modifications by [Mutsuo Saito and Makoto Matsumoto](http://www.math.sci.hiroshima-u.ac.jp/~m-mat/MT/XSADD/).
* `XoroShiro128Plus` is an implementation of the XorShift modifications by David Blackman and Sebastiano Vigna, adapted from a [C code reference implementation](http://xorshift.di.unimi.it/xoroshiro128plus.c).
* `SplitMix64` is an implementation of the algorithm by Sebastiano Vigna, adapted from a [C code reference implementation](http://xorshift.di.unimi.it/splitmix64.c).
