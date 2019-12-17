# MarchingCubes
An implementation of procedurally-generated terrain using the marching cubes method for 3D mesh generation.

Written by Dmitri Salov

## Overview
Marching cubes is a method used for generating 3D meshes using volumetric data. This volumetric data is represented by a collection of equidistant points in 3D space and a density value associated with each point. We then form "cubes" using a set of 8 points as the vertices of a cube and "march" through a section of 3D space, building a mesh using the densities of the cube vertices.

## Density Function
The density of a point is calculated using a density function that takes in 3D coordinates of a single point as an argument and returns a scalar, which is the density at that point. While the density function can return anything, using 3D Perlin noise is an example of a useful density function as it follows cohesion between nearby points, resulting in smooth-looking terrain that can be procedurally generated.

## Resources
Wikipedia: https://en.wikipedia.org/wiki/Marching_cubes
NVIDIA article describing implementation details of the algorithm: https://developer.nvidia.com/gpugems/GPUGems3/gpugems3_ch01.html
Episode of a fantastic YouTube series -- includes visualizations of the algorithm: https://www.youtube.com/watch?v=M3iI2l0ltbE&t=66s
