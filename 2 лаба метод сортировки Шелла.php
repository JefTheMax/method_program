# Язык PHP
<?php
$min = 1;
$max = 10;
$mas = [];
for ($i = 0; $i < $max; $i++) {
    array_push($mas, mt_rand($min, $max));
}
$cnt = 0;
$lent = count($mas);
$step = intdiv($lent, 2);
$steps = [];
echo "Изначальный массив $mas";
print_r($mas);
while ($step > 0) {
    for ($i = $step; $i < $lent; $i++) {
	$j = $i;
	$delta = $j - $step;
	while ($delta >= 0 && $mas[$delta] > $mas[$j]) {
	    $cnt++;
            $temp = $mas[$delta];
	    $mas[$delta] = $mas[$j];
	    $mas[$j] = $temp;
	    $j = $delta;
	    $delta = $j - $step;
	}
    }
    array_push($steps, $step);
    $step = intdiv($step, 2);
}
echo "Массив шагов ";
print_r($steps);
echo "\nОтсортированный массив ";
print_r($mas);
echo "\nКоличество сравнений $cnt";
?>