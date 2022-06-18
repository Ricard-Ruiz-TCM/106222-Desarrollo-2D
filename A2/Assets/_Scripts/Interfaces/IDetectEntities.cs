
public interface IDetectEntities<T> {

    T Entity { get; }
    bool EntityDetected { get; }
    float VisionAngle { get; }
    float DetectionRange { get; }

    void EntityDetection();

    bool EntityInRange(T entity);
    bool EntityInPOV(T entity);
    bool EntityIsVisible(T entity);
    bool EntityNoiseDetector(T entity);

}
