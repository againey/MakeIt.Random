camera
{
	orthographic
	location < 0, 0, -10 >
	look_at < 0, 0, 0 >
	right x * 10
	up y * image_height / image_width * 10
}

light_source { < -5, 5, -20 > rgb 1 parallel point_at < 0, 0, 0 > }

union
{
	box { < -4.5, -4.5, -0.5 >, < +4.5, +4.5, +0.5 > }
	cylinder { < -4.5, -4.5, 0 >, < -4.5, +4.5, 0 >, 0.5 }
	cylinder { < +4.5, -4.5, 0 >, < +4.5, +4.5, 0 >, 0.5 }
	cylinder { < -4.5, -4.5, 0 >, < +4.5, -4.5, 0 >, 0.5 }
	cylinder { < -4.5, +4.5, 0 >, < +4.5, +4.5, 0 >, 0.5 }
	sphere { < -4.5, -4.5, 0 >, 0.5 }
	sphere { < -4.5, +4.5, 0 >, 0.5 }
	sphere { < +4.5, -4.5, 0 >, 0.5 }
	sphere { < +4.5, +4.5, 0 >, 0.5 }
	
	texture
	{
		pigment
		{
			rgb < 1, 1, 1 > * 0.9
		}
		finish
		{
			ambient 0.5
		}
	}
}
