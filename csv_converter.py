#! /usr/bin/python3.5
import csv
from blist import sorteddict
from pprint import pprint
# import numpy as np

from math import sqrt

def distance(a, b):
	delta_x = a[0] - b[0]
	delta_y = a[1] - b[1]
	return sqrt(delta_x**2 + delta_y**2)

def main():
	with open("elevation_map2.csv") as infile:
		reader = csv.reader(infile)
		next(reader)
		coordinates = sorteddict({(float(x), float(y)): float(elevation) for (_, x, y, elevation, *_) in reader})

	max_x = max(key[0] for key in coordinates)
	min_x = min(key[0] for key in coordinates)

	max_y = max(key[1] for key in coordinates)
	min_y = min(key[1] for key in coordinates)

	print('max x coordinate is: {}'.format(max_x))
	print('min x coordinate is: {}'.format(min_x))

	print('max y coordinate is: {}'.format(max_y))
	print('min y coordinate is: {}'.format(min_y))


	x_divisions = 20
	x_div_size = (max_x - min_x) / x_divisions

	y_divisions = 20
	y_div_size = (max_y - min_y) / y_divisions

	elevations = []

	x_val = min_x
	for i in range(x_divisions):
		y_val = min_y
		elevation_val = []
		for i in range(y_divisions):
			this_location = (x_val, y_val)
			# elev_index = coordinates.keys().bisect(this_location)

			# elev_indices = [elev_index - 1, elev_index, elev_index + 1]
			# elev_indices = filter(lambda x: 0 <= x < len(coordinates), elev_indices)
			# elev_locations = map(lambda x: coordinates.keys()[x], elev_indices)
			# closest_location = min(elev_locations, key=lambda x: distance(x, this_location))

			# elevation_val.append(coordinates[closest_location])

			closest_location = min(coordinates, key=lambda x: distance(x, this_location))
			elevation_val.append(coordinates[closest_location])


			y_val += y_div_size

		elevations.append(elevation_val)
		x_val += x_div_size

	pprint(elevations)

	with open('output_elevations.csv', 'w') as outfile:
		writer = csv.writer(outfile)
		writer.writerows(elevations)

if __name__ == '__main__':
	main()