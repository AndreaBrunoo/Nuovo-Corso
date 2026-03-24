
# Comandi git

I comandi che contengono `< >` i valore al suo interno (Es: branch) indicano che in quel caso bisogna solo scrivere il nome del branch o comunque del valore all'interno dei `< >`, ovviamente i segni non andranno scritti quando andremo a lanciare il comando.

- `git switch <branch>` passa a un branch.
- `git switch -c <branch>` crea e passa al nuovo branch.
- `git pull --f-only` aggiorna il branch solo se può "avanzare diritto" (evita merge automatici).
- `git fetch (--all)` scarica aggiornamenti dal remoto senza toccare i file locali.
- `git add <file>` prepara i file per il commit.
- `git commit -m "messaggio"` registra le modifiche con un messaggio breve e chiaro.
- `git push -u origin <branch>` pubblica il branch e imposta il tracciamento con origin.
- `git branch -m <nuovo-nome>` rinomina un branch locale.
- `git push origin :<branch>` elimina un branch sul remoto.
- `git branch -d <branch>` elimina un branch locale già mergiato.
- `git revert <SHA>` annulla un commit già pubblicato creando un commit inverso.
- `git merge origin/<branch>` unisce nel tuo branch le ultime modifiche del branch remoto.
- `git tag -a vX.Y.Z -m "note"` crea un tag "di versione" con descrizione.

# Regole semplici per non pestarsi i piedi 

- Solo una persona crea il repository e i branch principali (main developer).
- La persona che crea il repository assegna i ruoli e le regole.
- Gli altri sviluppatori quando hanno fatto la modifica o il fix aprono una PR verso developer (pull request).
- Ogni task o modifica o fix deve essere più semplice possibile, nel caso separarle in parti più piccole e semplici.
- Bisogna necessariamente far procedere il lavoro e di conseguenza fare i merge di piccoli task semplici giornalmente , invece di aspettare di avere un task complesso completo per fare un unico merge.
- Il branch main è solo per il codice stabile e rilasciato, non ci si lavora direttamente.
- Solo una persona del team fa merge su main, dopo aver testato e verificato che è tutto ok.
- prima di creare una feature fai: `git switch developer && git pull --ff-only`.
- Una feature = un branch = una PR verso developer.
- Evita d modificare le stesse righe : se serve, parlatevi prima.
- Risolvi i conflitti in locale, poi aggiorna la PR.
- Niente push forzati su developer o main.
- Elimina i branch feature dopo il merge (locale e remoto).