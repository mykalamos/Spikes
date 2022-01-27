$alpha = @()
$alphaDict = [ordered]@{}
$n = 0;

[char[]](0..255) -cmatch '[a-z]' | % { 
    $c = $_.ToString().ToLower();
    $alpha += $c;
    $alphaDict.Add($c, $n++);
}

#$alpha
#$alphaDict['x']

function Rot
{
    Param
    (
        [int]$offset,
        [string] $inputString
    )

    [int] $max =  $alphaDict.Count - 1;
    [string] $s = '';
    foreach ($char in $inputString.GetEnumerator()) 
    {
        [int] $i = $alphaDict[$char.ToString()];
        [int] $j = ($i + $offset) % $max;
        $s += $alpha[$j];
    }
    return $s;
}

function RotEquivalent
{
    Param(
        [string]$lhs,
        [string]$rhs
    )
    if($lhs.Length -ne $rhs.Length)
    {
        return $false;
    }
    $length = $lhs.Length;
    [int] $max =  $alphaDict.Count - 1;

    for($x=0; $x -lt $length; $x++)
    {
        if($x -eq $length-1){
            break;
        }
        [string] $lhsChar = $lhs[$x];
        [string] $lhsCharNext = $lhs[$x+1];
        [string] $rhsChar = $rhs[$x];
        [string] $rhsCharNext = $rhs[$x+1];
        
        [int] $o1 = ($alphaDict[$lhsCharNext]-$alphaDict[$lhsChar]) % $max;
        [int] $o2 = ($alphaDict[$rhsCharNext]-$alphaDict[$rhsChar]) % $max;
        if($o1 -ne $o2){
            return $false;
        }
    }
    return $true;
}



'-' * 20
#Rot 13 'aa'
#Rot 13 'bb'
#Rot 13 'xyz'
#Rot -1 'a'
#Rot -1 'abc'

RotEquivalent 'abc' 'def'
RotEquivalent 'abcd' 'def'
RotEquivalent 'zab' 'xyz'
RotEquivalent 'ace' 'xzb'
RotEquivalent 'dish' 'plat'
RotEquivalent 'dish' 'plae'