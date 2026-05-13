using System.Globalization;
using batch_processing.Models;

namespace batch_processing.Services;

public sealed class UiTextService
{
    private static readonly IReadOnlyDictionary<string, string> English = new Dictionary<string, string>
    {
        ["app.meta.title"] = "Batch Processing Simulator",
        ["app.layout.eyebrow"] = "Systems Portfolio · Blazor WebAssembly",
        ["app.layout.title"] = "Batch Processing",
        ["app.layout.subtitle"] = "Interactive operating systems simulator focused on fixed-size batch execution.",
        ["app.layout.batchSize"] = "Batch size: 4 processes",
        ["app.layout.theme"] = "Theme",
        ["app.layout.language"] = "Language",
        ["app.layout.toolbar"] = "Application toolbar",
        ["app.credit.line"] = "Developed by Axel Navor.",
        ["app.credit.aria"] = "Developer credit",
        ["app.theme.dark"] = "Dark mode",
        ["app.theme.light"] = "Light mode",
        ["app.language.english"] = "English",
        ["app.language.spanish"] = "Spanish",
        ["app.language.german"] = "German",

        ["header.status.idle"] = "Ready to capture",
        ["header.status.running"] = "Simulation running",
        ["header.status.completed"] = "Completed and paused",
        ["header.status.unknown"] = "Unknown status",
        ["header.kpi.globalCounter"] = "Global Counter",
        ["header.kpi.pendingBatches"] = "Pending Batches",
        ["header.kpi.totalBatches"] = "Total Batches",
        ["header.kpi.completedProcesses"] = "Completed Processes",

        ["capture.title"] = "Process Capture",
        ["capture.description"] = "Add the processes from the keyboard in the exact order they should be executed.",
        ["capture.captured"] = "{0} captured",
        ["capture.field.programmerName"] = "Programmer Name",
        ["capture.field.programId"] = "Program ID",
        ["capture.field.estimatedMaxTime"] = "Estimated Max Time",
        ["capture.field.operation"] = "Operation",
        ["capture.field.operandA"] = "Operand A",
        ["capture.field.operandB"] = "Operand B",
        ["capture.button.open"] = "New Process",
        ["capture.button.add"] = "Add Process",
        ["capture.queue.title"] = "Captured Queue",
        ["capture.queue.description"] = "This list defines the order used to build the batches.",
        ["capture.queue.empty"] = "No processes captured yet.",

        ["controls.title"] = "Simulation Controls",
        ["controls.description"] = "Start the simulation when the capture phase is complete or reset everything to load a new dataset.",
        ["controls.button.start"] = "Start Simulation",
        ["controls.button.reset"] = "Reset",
        ["controls.status.idleEmpty"] = "Capture the processes that will form the execution batches.",
        ["controls.status.ready"] = "{0} processes captured and ready to run.",
        ["controls.status.running"] = "Executing batch {0}.",
        ["controls.status.completed"] = "All batches finished. The simulation remains paused for inspection.",
        ["controls.completed.note"] = "Execution finished and paused on screen for review.",

        ["batch.title"] = "Current Batch",
        ["batch.description"] = "Processes currently loaded in the active batch.",
        ["batch.empty.idle"] = "The active batch will appear here after the simulation starts.",
        ["batch.empty.completed"] = "No active batch. Review the final results below.",
        ["batch.estimatedTime"] = "Estimated time: {0} s",

        ["running.title"] = "Running Process",
        ["running.description"] = "Detailed view of the process currently consuming CPU time.",
        ["running.empty.idle"] = "The simulator has not started yet.",
        ["running.empty.completed"] = "All processes finished. The final state is paused for inspection.",
        ["running.name"] = "Name",
        ["running.batch"] = "Batch",
        ["running.operation"] = "Operation",
        ["running.estimatedMaxTime"] = "Estimated Max Time",
        ["running.elapsed"] = "Elapsed Time",
        ["running.remaining"] = "Remaining Time",
        ["running.progress"] = "Progress",

        ["finished.title"] = "Finished Processes",
        ["finished.description"] = "Completed results plus explicit markers when one batch ends and the next one begins.",
        ["finished.entries"] = "{0} entries",
        ["finished.empty"] = "Completed processes will be listed here as the simulation advances.",
        ["finished.result"] = "Result: {0}",
        ["finished.batchStarted"] = "Batch {0} started",
        ["finished.batchCompleted"] = "Batch {0} completed",
        ["finished.programTitle"] = "Program #{0}",
        ["finished.duration"] = "Execution time: {0} s",

        ["general.batch"] = "Batch {0}",
        ["general.program"] = "Program #{0}",
        ["general.seconds.short"] = "{0} s",
        ["general.cancel"] = "Cancel",

        ["status.pending"] = "Pending",
        ["status.ready"] = "Ready",
        ["status.running"] = "Running",
        ["status.completed"] = "Completed",

        ["operation.add"] = "Addition",
        ["operation.subtract"] = "Subtraction",
        ["operation.multiply"] = "Multiplication",
        ["operation.divide"] = "Division",
        ["operation.modulo"] = "Modulo",
        ["operation.power"] = "Power",

        ["validation.programmerNameRequired"] = "Programmer name is required.",
        ["validation.programIdPositive"] = "Program ID must be greater than 0.",
        ["validation.programIdUnique"] = "Program ID must be unique.",
        ["validation.estimatedMaxTimePositive"] = "Estimated max time must be greater than 0.",
        ["validation.operandsValid"] = "Operands must be valid numeric values.",
        ["validation.divisionNonZero"] = "Division and modulo require a non-zero second operand.",
        ["validation.moduloInteger"] = "Modulo requires integer operands."
    };

    private static readonly IReadOnlyDictionary<string, string> Spanish = new Dictionary<string, string>
    {
        ["app.meta.title"] = "Simulador de Procesamiento por Lotes",
        ["app.layout.eyebrow"] = "Portafolio de Sistemas · Blazor WebAssembly",
        ["app.layout.title"] = "Procesamiento por Lotes",
        ["app.layout.subtitle"] = "Simulador interactivo de sistemas operativos enfocado en ejecución por lotes de tamaño fijo.",
        ["app.layout.batchSize"] = "Tamaño del lote: 4 procesos",
        ["app.layout.theme"] = "Tema",
        ["app.layout.language"] = "Idioma",
        ["app.layout.toolbar"] = "Barra de la aplicación",
        ["app.credit.line"] = "Desarrollado por Axel Navor.",
        ["app.credit.aria"] = "Crédito del desarrollador",
        ["app.theme.dark"] = "Modo oscuro",
        ["app.theme.light"] = "Modo claro",
        ["app.language.english"] = "Inglés",
        ["app.language.spanish"] = "Español",
        ["app.language.german"] = "Alemán",

        ["header.status.idle"] = "Listo para capturar",
        ["header.status.running"] = "Simulación en ejecución",
        ["header.status.completed"] = "Finalizado y pausado",
        ["header.status.unknown"] = "Estado desconocido",
        ["header.kpi.globalCounter"] = "Contador Global",
        ["header.kpi.pendingBatches"] = "Lotes Pendientes",
        ["header.kpi.totalBatches"] = "Lotes Totales",
        ["header.kpi.completedProcesses"] = "Procesos Terminados",

        ["capture.title"] = "Captura de Procesos",
        ["capture.description"] = "Agrega los procesos desde teclado en el orden exacto en que deben ejecutarse.",
        ["capture.captured"] = "{0} capturados",
        ["capture.field.programmerName"] = "Nombre del Programador",
        ["capture.field.programId"] = "Número de Programa",
        ["capture.field.estimatedMaxTime"] = "Tiempo Máximo Estimado",
        ["capture.field.operation"] = "Operación",
        ["capture.field.operandA"] = "Operando A",
        ["capture.field.operandB"] = "Operando B",
        ["capture.button.open"] = "Nuevo proceso",
        ["capture.button.add"] = "Agregar Proceso",
        ["capture.queue.title"] = "Cola Capturada",
        ["capture.queue.description"] = "Esta lista define el orden usado para construir los lotes.",
        ["capture.queue.empty"] = "Aún no hay procesos capturados.",

        ["controls.title"] = "Controles de Simulación",
        ["controls.description"] = "Inicia la simulación cuando la captura termine o reinicia todo para cargar un nuevo conjunto de datos.",
        ["controls.button.start"] = "Iniciar Simulación",
        ["controls.button.reset"] = "Reiniciar",
        ["controls.status.idleEmpty"] = "Captura los procesos que formarán los lotes de ejecución.",
        ["controls.status.ready"] = "{0} procesos capturados y listos para ejecutar.",
        ["controls.status.running"] = "Ejecutando el lote {0}.",
        ["controls.status.completed"] = "Todos los lotes terminaron. La simulación permanece pausada para observación.",
        ["controls.completed.note"] = "La ejecución terminó y quedó pausada en pantalla para revisión.",

        ["batch.title"] = "Lote en Ejecución",
        ["batch.description"] = "Procesos cargados actualmente en el lote activo.",
        ["batch.empty.idle"] = "El lote activo aparecerá aquí cuando inicie la simulación.",
        ["batch.empty.completed"] = "No hay lote activo. Revisa abajo los resultados finales.",
        ["batch.estimatedTime"] = "Tiempo estimado: {0} s",

        ["running.title"] = "Proceso en Ejecución",
        ["running.description"] = "Vista detallada del proceso que está consumiendo tiempo de CPU.",
        ["running.empty.idle"] = "La simulación aún no ha comenzado.",
        ["running.empty.completed"] = "Todos los procesos terminaron. El estado final quedó pausado para inspección.",
        ["running.name"] = "Nombre",
        ["running.batch"] = "Lote",
        ["running.operation"] = "Operación",
        ["running.estimatedMaxTime"] = "Tiempo Máximo Estimado",
        ["running.elapsed"] = "Tiempo Transcurrido",
        ["running.remaining"] = "Tiempo Restante",
        ["running.progress"] = "Progreso",

        ["finished.title"] = "Procesos Terminados",
        ["finished.description"] = "Resultados completados junto con marcas explícitas cuando termina un lote y comienza el siguiente.",
        ["finished.entries"] = "{0} registros",
        ["finished.empty"] = "Los procesos terminados aparecerán aquí conforme avance la simulación.",
        ["finished.result"] = "Resultado: {0}",
        ["finished.batchStarted"] = "Inició el lote {0}",
        ["finished.batchCompleted"] = "Terminó el lote {0}",
        ["finished.programTitle"] = "Programa #{0}",
        ["finished.duration"] = "Tiempo de ejecución: {0} s",

        ["general.batch"] = "Lote {0}",
        ["general.program"] = "Programa #{0}",
        ["general.seconds.short"] = "{0} s",
        ["general.cancel"] = "Cancelar",

        ["status.pending"] = "Pendiente",
        ["status.ready"] = "Listo",
        ["status.running"] = "En ejecución",
        ["status.completed"] = "Terminado",

        ["operation.add"] = "Suma",
        ["operation.subtract"] = "Resta",
        ["operation.multiply"] = "Multiplicación",
        ["operation.divide"] = "División",
        ["operation.modulo"] = "Residuo",
        ["operation.power"] = "Potencia",

        ["validation.programmerNameRequired"] = "El nombre del programador es obligatorio.",
        ["validation.programIdPositive"] = "El número de programa debe ser mayor que 0.",
        ["validation.programIdUnique"] = "El número de programa debe ser único.",
        ["validation.estimatedMaxTimePositive"] = "El tiempo máximo estimado debe ser mayor que 0.",
        ["validation.operandsValid"] = "Los operandos deben ser valores numéricos válidos.",
        ["validation.divisionNonZero"] = "La división y el residuo requieren un segundo operando distinto de cero.",
        ["validation.moduloInteger"] = "El residuo requiere operandos enteros."
    };

    private static readonly IReadOnlyDictionary<string, string> German = new Dictionary<string, string>
    {
        ["app.meta.title"] = "Stapelverarbeitungs-Simulator",
        ["app.layout.eyebrow"] = "Systemportfolio · Blazor WebAssembly",
        ["app.layout.title"] = "Stapelverarbeitung",
        ["app.layout.subtitle"] = "Interaktiver Betriebssystem-Simulator mit Fokus auf Ausfuehrung in festen Stapeln.",
        ["app.layout.batchSize"] = "Stapelgroesse: 4 Prozesse",
        ["app.layout.theme"] = "Thema",
        ["app.layout.language"] = "Sprache",
        ["app.layout.toolbar"] = "Anwendungsleiste",
        ["app.credit.line"] = "Entwickelt von Axel Navor.",
        ["app.credit.aria"] = "Entwicklerhinweis",
        ["app.theme.dark"] = "Dunkler Modus",
        ["app.theme.light"] = "Heller Modus",
        ["app.language.english"] = "Englisch",
        ["app.language.spanish"] = "Spanisch",
        ["app.language.german"] = "Deutsch",

        ["header.status.idle"] = "Bereit zur Eingabe",
        ["header.status.running"] = "Simulation laeuft",
        ["header.status.completed"] = "Abgeschlossen und pausiert",
        ["header.status.unknown"] = "Unbekannter Status",
        ["header.kpi.globalCounter"] = "Globaler Zaehler",
        ["header.kpi.pendingBatches"] = "Ausstehende Stapel",
        ["header.kpi.totalBatches"] = "Gesamtstapel",
        ["header.kpi.completedProcesses"] = "Abgeschlossene Prozesse",

        ["capture.title"] = "Prozesserfassung",
        ["capture.description"] = "Fuege die Prozesse in genau der Reihenfolge hinzu, in der sie ausgefuehrt werden sollen.",
        ["capture.captured"] = "{0} erfasst",
        ["capture.field.programmerName"] = "Name des Programmierers",
        ["capture.field.programId"] = "Programmnummer",
        ["capture.field.estimatedMaxTime"] = "Geschaetzte Maximalzeit",
        ["capture.field.operation"] = "Operation",
        ["capture.field.operandA"] = "Operand A",
        ["capture.field.operandB"] = "Operand B",
        ["capture.button.open"] = "Neuer Prozess",
        ["capture.button.add"] = "Prozess hinzufuegen",
        ["capture.queue.title"] = "Erfasste Warteschlange",
        ["capture.queue.description"] = "Diese Liste bestimmt die Reihenfolge zur Bildung der Stapel.",
        ["capture.queue.empty"] = "Noch keine Prozesse erfasst.",

        ["controls.title"] = "Simulationssteuerung",
        ["controls.description"] = "Starte die Simulation nach Abschluss der Eingabe oder setze alles fuer neue Daten zurueck.",
        ["controls.button.start"] = "Simulation starten",
        ["controls.button.reset"] = "Zuruecksetzen",
        ["controls.status.idleEmpty"] = "Erfasse die Prozesse, die die Ausfuehrungsstapel bilden sollen.",
        ["controls.status.ready"] = "{0} Prozesse erfasst und bereit zur Ausfuehrung.",
        ["controls.status.running"] = "Stapel {0} wird ausgefuehrt.",
        ["controls.status.completed"] = "Alle Stapel sind beendet. Die Simulation bleibt zur Beobachtung pausiert.",
        ["controls.completed.note"] = "Die Ausfuehrung ist beendet und zur Ansicht pausiert.",

        ["batch.title"] = "Aktueller Stapel",
        ["batch.description"] = "Prozesse, die aktuell im aktiven Stapel geladen sind.",
        ["batch.empty.idle"] = "Der aktive Stapel erscheint hier nach dem Start der Simulation.",
        ["batch.empty.completed"] = "Kein aktiver Stapel. Unten koennen die Endergebnisse geprueft werden.",
        ["batch.estimatedTime"] = "Geschaetzte Zeit: {0} s",

        ["running.title"] = "Laufender Prozess",
        ["running.description"] = "Detailansicht des Prozesses, der aktuell CPU-Zeit verbraucht.",
        ["running.empty.idle"] = "Die Simulation wurde noch nicht gestartet.",
        ["running.empty.completed"] = "Alle Prozesse sind abgeschlossen. Der Endzustand ist zur Inspektion pausiert.",
        ["running.name"] = "Name",
        ["running.batch"] = "Stapel",
        ["running.operation"] = "Operation",
        ["running.estimatedMaxTime"] = "Geschaetzte Maximalzeit",
        ["running.elapsed"] = "Verstrichene Zeit",
        ["running.remaining"] = "Verbleibende Zeit",
        ["running.progress"] = "Fortschritt",

        ["finished.title"] = "Abgeschlossene Prozesse",
        ["finished.description"] = "Abgeschlossene Ergebnisse plus deutliche Marker fuer Stapelende und Stapelbeginn.",
        ["finished.entries"] = "{0} Eintraege",
        ["finished.empty"] = "Abgeschlossene Prozesse werden hier waehrend der Simulation angezeigt.",
        ["finished.result"] = "Ergebnis: {0}",
        ["finished.batchStarted"] = "Stapel {0} gestartet",
        ["finished.batchCompleted"] = "Stapel {0} abgeschlossen",
        ["finished.programTitle"] = "Programm #{0}",
        ["finished.duration"] = "Laufzeit: {0} s",

        ["general.batch"] = "Stapel {0}",
        ["general.program"] = "Programm #{0}",
        ["general.seconds.short"] = "{0} s",
        ["general.cancel"] = "Abbrechen",

        ["status.pending"] = "Ausstehend",
        ["status.ready"] = "Bereit",
        ["status.running"] = "Laeuft",
        ["status.completed"] = "Abgeschlossen",

        ["operation.add"] = "Addition",
        ["operation.subtract"] = "Subtraktion",
        ["operation.multiply"] = "Multiplikation",
        ["operation.divide"] = "Division",
        ["operation.modulo"] = "Modulo",
        ["operation.power"] = "Potenz",

        ["validation.programmerNameRequired"] = "Der Name des Programmierers ist erforderlich.",
        ["validation.programIdPositive"] = "Die Programmnummer muss groesser als 0 sein.",
        ["validation.programIdUnique"] = "Die Programmnummer muss eindeutig sein.",
        ["validation.estimatedMaxTimePositive"] = "Die geschaetzte Maximalzeit muss groesser als 0 sein.",
        ["validation.operandsValid"] = "Die Operanden muessen gueltige numerische Werte sein.",
        ["validation.divisionNonZero"] = "Division und Modulo erfordern einen zweiten Operanden ungleich null.",
        ["validation.moduloInteger"] = "Modulo erfordert ganzzahlige Operanden."
    };

    public string Get(AppLanguage language, string key)
    {
        var dictionary = language switch
        {
            AppLanguage.Spanish => Spanish,
            AppLanguage.German => German,
            _ => English
        };

        if (dictionary.TryGetValue(key, out var value))
        {
            return value;
        }

        return English.TryGetValue(key, out var fallback) ? fallback : key;
    }

    public string Format(AppLanguage language, string key, params object[] arguments)
    {
        return string.Format(CultureInfo.InvariantCulture, Get(language, key), arguments);
    }
}
