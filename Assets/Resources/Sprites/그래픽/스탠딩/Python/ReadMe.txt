!알아야 할 사항!

1. 모든 방향(왼쪽, 오른쪽)은 캐릭터 기준이 아닌 우리가 볼 때 기준으로 씀

2. 레이어 순서: 맨 아래 Body - Eyebrows/Eyes/Mouth - Effect(Flush/Anger/Hate/Shadow) - Effect(Sweat) - Effect(Tears) 맨 위

3. 파일 이름: '캐릭터_파츠_번호_이름(_기타)'로 저장
Ex)	Python_Effect_02_Flush3

4-1. Eyes 파일 이름: '캐릭터_Eyes_번호("눈 모양 번호"+"눈동자 모양 번호")_눈 모양_눈동자 모양_눈동자 위치'로 저장
Ex)	Python_Eyes_00_Default1_Default_Front - 파이썬의 기본 눈, 기본 눈동자, 정면
	Java_Eyes_13_Surprised_Serious_Left - 자바의 놀란 눈, 심각한 눈동자, 왼쪽

4-2. Eyes 눈 모양 중 Smiling은 눈동자가 없음. 눈동자 모양 번호 9로 저장
Ex)	Python_Eyes_49_Smiling1



<Python>

5. 파이썬 눈 모양의 1과 2는 아이홀 위치가 다름
Eyebrows가 Angry일 때만 2, 나머지는 1
Ex)	Eyebrows가 Default 등일 때 - Python_Eyes_00_Default1_Default_Front 사용
	Eyebrows가 Angry일 때 - Python_Eyes_00_Default2_Default_Front 사용

6. Python_Effect_00_Flush1은 눈 모양에 따라 네 종류로 나뉨
Python_Effect_00_Flush1_0: 0_Default 기본, 1_Surprised 놀람, 2_HalfClosed 반 감음
Python_Effect_00_Flush1_1: 3_HalfSmiling 반 웃음
Python_Effect_00_Flush1_2: 4_Smiling 웃음
Python_Effect_00_Flush1_3: 5_Winking 윙크

7. Python_Effect_01_Flush2는 눈 모양에 따라 세 종류로 나뉨
Python_Effect_01_Flush2_0: 0_Default 기본, 1_Surprised 놀람, 2_HalfClosed 반 감음, 3_HalfSmiling 반 웃음
Python_Effect_01_Flush2_1: 4_Smiling 웃음
Python_Effect_01_Flush2_2: 5_Winking 윙크

8. Python_Effect_08_Tears는 눈 모양에 따라 네 종류로 나뉨 (Flush1과 동일)
Python_Effect_08_Tears_0: 0_Default 기본, 1_Surprised 놀람, 2_HalfClosed 반 감음
Python_Effect_08_Tears_1: 3_HalfSmiling 반 웃음
Python_Effect_08_Tears_2: 4_Smiling 웃음
Python_Effect_08_Tears_3: 5_Winking 윙크



------------------------------------------------------------------------------------------------------------------------------------------



* Eyes 눈 모양 정리
0_Default 기본
1_Surprised 놀람
2_HalfClosed 반 감음
3_HalfSmiling 반 웃음
4_Smiling 웃음
5_Winking 윙크

* Eyes 눈동자 모양 정리
0_Default 기본
1_Wet 촉촉
2_Dry 건조
3_Serious 정색

* Eyebrows 정리
0_Default 기본
1_Surprised 놀람
2_Angry 화남
3_Sad 슬픔
4_Questionable 의문
5_Frown 찡그림

* Mouth 정리
0_Speaking 대화
1_Open 열림
2_Closed 닫힘
3_Smiling 미소
4_Laughing 웃음
5_Shouting 외침
6_Pouting 삐죽

* Effect 정리
00_Flush1 그라데이션 홍조, 볼
01_Flush2 빗금 홍조, 볼
02_Flush3 빗금 홍조, 코/귀
03_Anger 화난 표시
04_Hate 세로 빗금
05_Shadow 눈 밑까지 그림자
06_Sweat1 땀 한 방울
07_Sweat2 땀 여러 방울
08_Tears 눈에 고인 눈물 (눈 모양마다)
09_Teardrop1 흐르는 눈물, 왼쪽 볼
10_Teardrop2 흐르는 눈물, 오른쪽 볼
11_Teardrop3 떨어지는 눈물, 턱