using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


using _Project.Scripts.Character.Runtime;
using NSubstitute;

using VContainer.Unity;
using _Project.Scripts.Character.Runtime.Controllers;
using VContainer;
using UnityEngine.SceneManagement;

public class PlayerTest
{
    PlayerSM playerSM;
    [SetUp]
    public void Test1()
    {

    }
    [UnityTest]
    public IEnumerator Test2()
    {
        SceneManager.LoadScene("Startup");
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "GamePlay");
        playerSM = GameObject.Find("Player").GetComponent<PlayerSM>();
        Assert.IsNotNull(playerSM);
    }
}
/*
 private GameObject gameObject;
    private PlayerSM playerSM;
    private CharacterGraphics mockCharacterGraphics;
    private BaseGunBehavior mockGunBehavior;
    private EnemyDetector mockEnemyDetector;
    private PlayerMovementController mockPlayerMovementController;
    private DetectionController mockDetectionController;

    [SetUp]
    public void Setup()
    {
        gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        playerSM = gameObject.AddComponent<PlayerSM>();

        // Mock nesneler oluştur
        mockCharacterGraphics = Substitute.For<CharacterGraphics>();
        mockGunBehavior = Substitute.For<BaseGunBehavior>();
        mockEnemyDetector = Substitute.For<EnemyDetector>();
        mockPlayerMovementController = Substitute.For<PlayerMovementController>();
        mockDetectionController = Substitute.For<DetectionController>();
        
        // VContainer ile bağımlılıkları kaydet
        var scope = LifetimeScope.Create(builder =>
        {
            builder.RegisterInstance(mockCharacterGraphics);
            builder.RegisterInstance(mockGunBehavior);
            builder.RegisterInstance(mockEnemyDetector);
            builder.RegisterInstance(mockPlayerMovementController);
            builder.RegisterInstance(mockDetectionController);

            // .RegisterInstance(mockGunBehavior)
            // .RegisterInstance(mockEnemyDetector)
            // .RegisterInstance(mockPlayerMovementController)
            // .RegisterInstance(mockDetectionController)
            // .Build();

        });

        // .RegisterInstance(mockCharacterGraphics)
        // .RegisterInstance(mockGunBehavior)
        // .RegisterInstance(mockEnemyDetector)
        // .RegisterInstance(mockPlayerMovementController)
        // .RegisterInstance(mockDetectionController)
        // .Build();

        // Bağımlılıkları PlayerSM nesnesine enjekte et
        playerSM.InjectDependenciesAndInitialize(scope);
    }

    [Test]
    public void TestPlayerInitialization()
    {
        // Initialize veya AWAKE_TEST çağır
        // playerSM.AWAKE_TEST();

        // // Mock nesneler üzerinde beklenen işlemleri doğrula
        // mockCharacterGraphics.Received().Initialise(playerSM);
        // mockGunBehavior.Received().InitialiseCharacter(Arg.Any<CharacterGraphics>(), playerSM);
        // mockEnemyDetector.Received().Initialise(Arg.Any<DetectionController>());
        // mockPlayerMovementController.Received().Initialise();
    }

    [TearDown]
    public void Teardown()
    {
        if (gameObject != null)
            Object.DestroyImmediate(gameObject);
    }*/