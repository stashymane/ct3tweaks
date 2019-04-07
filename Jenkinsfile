pipeline {
  agent any
  stages {
    stage('Restore Packages') {
      steps {
        sh 'nuget restore'
      }
    }
    stage('Build') {
      steps {
        sh 'xbuild ct3tweaks.sln'
      }
    }
    stage('Archive') {
      steps {
        archiveArtifacts 'ct3tweaks/bin/CT3Tweaks.exe,ct3tweaks/bin/CT3Tweaks.exe.config'
      }
    }
  }
}