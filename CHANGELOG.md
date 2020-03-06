# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and starting from v3.0.0, this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [2.0.0] - 2017-01-14

Coincided with release of Make It Colorful v1.1.

### Added

* Distribution sampling, with initial support for continuous sampling from the following:
  * uniform distribution
  * triangular distribution
  * trapezoidal distribution
  * linear distribution
  * Hermite spline distribution
  * normal distribution
  * exponential distribution
  * piecewise uniform distribution
  * piecewise linear distribution
  * piecewise Hermite spline distribution
* Common color categories, such as bold, pastel, and cool.
* `float` and `double` overloads for `RandomListAccess.WeightedElement(...)`
* Distribution sampling demo scene.

### Changed

* Changed the `elementCount` parameter order for `RandomListAccess.WeightedIndex(...)` for better consistency and flexibility.
* `MIRandom.CreateStandard(...)` functions now return `IRandom` instead of `XorShift128Plus`.
* `MIRandom.CreateStandard(...)` functions may return an instance of some engine type other than `XorShift128Plus`, depending on the platform and compiler defines.
* Improved weighted list index generation to only generate a single random value, instead of one per each weight.
* Improved pre-computed weighted list index generation to operate in O(log n) time instead of O(n), by storing a cumulative weight sum array and performing a binary search on it.
* Provided additional overloads for weighted list index/element generation to specify a subset of weights to use, convenient for manually resized arrays that may have more elements than are currently used.
* Added `WeightedRandomIndex` and `WeightedRandomElement` extension functions for `IList<T>`.
* Random engines now support equality comparisons, which check that both type and internal state are equal.
* Parameter requirements for `RandomInteger.RangeCO(...)` using unsigned types loosened; an upper exclusive value of 0 will now generate a value within the entire range of the unsigned integer type.
* The shuffle and weighted index generation demo will now repeatedly select new indices at a configurable frequency.
* The random colors demo allows a more flexible method of generating colors from a selected color category.

### Fixed

* Control flow in `RandomGeometry.Rotation()` tweaked so that the updated compiler in Unity 5.5 will not complain about an unset out parameter.
* Fixed the version 0.1 backward-compatible code path for seeding `XorShift128Plus` so that it works in both the Complete and Basic edition by not depending on the existence of `SplitMix64`.
* Corrected the version 0.1 backward-compatible code path for `RandomGeometry.UnitVector2()` to do the proper math instead of calling itself repeatedly and cause a stack overflow.

## [1.0.0] - 2016-11-18

Coincided with release of Make It Tiled v2.0 and Make It Colorful v1.0.

### Added

#### Architecture

* `IRandom` interface
* Random state generator
* Random generator pattern
* `MIRandom` static helper class
* Range bound inclusivity naming pattern

#### Engines

* XorShift1024*
* XoroShiro128+
* XorShiftAdd
* `UnityEngine.Random` wrapper

#### Features

* bits
* more integer types
* angles
* 4D vectors and quaternions
* more string varieties
* dice rolls
* colors
* more probability varieties
* selecting elements from a list
* selecting values from an enumeration
* selecting values from the set {-1, 0, +1}
* shuffling a list

#### Demos

* vectors and quaternions
* strings
* dice rolls
* colors
* shuffling and weighted element selection
* performance measurement

### Changed

* Radical overhaul to nearly everything
  * namespaces
  * class names
  * function names
  * function parameters

## [0.1.0] - 2016-04-08

Coincided with release of Tile-Based Worlds v1.0

### Added

#### Architecture

* `IRandomEngine` interface
* `RandomSeedUtility` state generator

#### Engines

* XorShift128+
* SplitMix64
* `System.Random` wrapper

#### Features

* closed and half-open ranges for:
  * int
  * uint
  * float
  * double
* floating point numbers
* closed and half-open unit ranges for:
  * float
  * double
* 2D and 3D vectors
* hexadecimal strings
* evaluating probabilities
* weighted indexes
